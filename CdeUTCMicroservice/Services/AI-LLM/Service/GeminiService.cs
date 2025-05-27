using AI_LLM.Entities;
using AI_LLM.Repository;
using AI_LLM.Setting;
using BuildingBlocks.ApiResponse;
using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AI_LLM.Service
{
    public interface IGeminiService
    {
        Task<ApiResponse<string>> AskGeminiAsync(GeminiAskRequest geminiAsk);
        Task<ApiResponse<List<Conversation>>> GetConversationByProjectId(int projectId);
    }
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClientGemini;
        private readonly HttpClient _httpClientProject;
        private readonly GeminiApiSetting _settings;
        private readonly string _systemContext;
        private readonly IBaseRepository<Entities.Message> _messageRepository;
        private readonly IBaseRepository<Conversation> _conversationRepository;

        public GeminiService(
            HttpClient httpClientGemini,
            HttpClient httpClientProject,
            IOptions<GeminiApiSetting> settings,
            IWebHostEnvironment env,
            IBaseRepository<Entities.Message> messageRepository,
            IBaseRepository<Conversation> conversationRepository)
        {
            _httpClientGemini = httpClientGemini;
            _httpClientProject = httpClientProject;
            _settings = settings.Value;
            _httpClientProject.BaseAddress = new Uri("https://localhost:5052/");
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;

            var contextPath = Path.Combine(env.WebRootPath, "system_context.json");
            _systemContext = File.ReadAllText(contextPath);
        }

        private string GenerateContext(int projectId, ProjectResponse projectResponse)
        {
            return $@"Thông tin dự án:
            - ID: {projectId}
            - Tên dự án: {projectResponse.Name}
            - Mô tả: {projectResponse.Description}
            - Số file: {projectResponse.FileCount}
            - Số folder: {projectResponse.FolderCount}
            - Số người dùng trong dự án: {projectResponse.UserCount}
            - Ngày bắt đầu dự án: {projectResponse.StartDate}
            - Ngày kết thúc dự án: {projectResponse.EndDate}
            - Size của dự án: {projectResponse.Size}

            Ngữ cảnh hệ thống:
            {_systemContext}";
        }

        private async Task<string> GenerateTitleAsync(string question)
        {
            var titlePrompt = $@"Dựa vào câu hỏi sau, hãy tạo một tiêu đề ngắn gọn (không quá 50 ký tự) cho cuộc hội thoại này:
            Câu hỏi: {question}
            
            Chỉ trả về tiêu đề, không thêm bất kỳ giải thích nào.";

            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = titlePrompt }
                        }
                    }
                }
            };

            var response = await _httpClientGemini.PostAsJsonAsync($"{_settings.BaseUrl}/models/gemini-2.0-flash:generateContent?key={_settings.ApiKey}", payload);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
            return jsonResponse.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString() ?? "Cuộc hội thoại mới";
        }

        public async Task<ApiResponse<string>> AskGeminiAsync(GeminiAskRequest geminiAskRequest)
        {
            var transaction = await _conversationRepository.BeginTransactionAsync(CancellationToken.None);
            int projectId = geminiAskRequest.ProjectId;
            string question = geminiAskRequest.Question;
            int conversationId = geminiAskRequest.ConversationId ?? 0;
            var response = await _httpClientProject.GetAsync($"projects/{projectId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            var projectResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ProjectResponse>>();
            if (projectResponse == null)
            {
                throw new NotFoundException(BuildingBlocks.Message.NOT_FOUND);
            }
            var project = projectResponse.Data;
            if (project == null)
            {
                throw new NotFoundException(BuildingBlocks.Message.NOT_FOUND);
            }

            var context = GenerateContext(projectId, project);

            // Kiểm tra xem đây có phải câu hỏi đầu tiên không
            var existingConversation = _conversationRepository.GetAllQueryAble()
                .FirstOrDefault(c => c.Id == conversationId && c.IsActive);

            Conversation conversation;
            if (existingConversation == null)
            {
                // Tạo title cho conversation mới
                var title = await GenerateTitleAsync(question);
                conversation = new Conversation
                {
                    Title = title,
                    ProjectId = projectId,
                    IsActive = true,
                    Context = context
                };
                await _conversationRepository.AddAsync(conversation, CancellationToken.None);
                await _conversationRepository.SaveChangeAsync(CancellationToken.None);
            }
            else
            {
                conversation = existingConversation;
            }

            //Lưu câu hỏi người dùng 
            await _messageRepository.AddAsync(new Entities.Message
            {
                ConversationId = conversation.Id,
                ProjectId = projectId,
                Content = question,
                IsAI = false
            }, CancellationToken.None);
            await _messageRepository.SaveChangeAsync(CancellationToken.None);

            // Lấy lịch sử liên quan đến dự án
            var history = _messageRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == projectId && conversationId == conversation.Id)
                .OrderByDescending(e => e.CreatedAt)
                .Take(5)
                .Select(q => $"{(q.IsAI ? 'A' : 'Q')}: {q.Content}\n")
                .ToList();

            // Cải thiện prompt
            var prompt = $@"Bạn là một trợ lý AI thông minh và chuyên nghiệp trong lĩnh vực quản lý dự án xây dựng. Hãy trả lời câu hỏi dựa trên các thông tin sau:

            NGỮ CẢNH CUỘC HỘI THOẠI:
            {conversation.Context}

            LỊCH SỬ TƯƠNG TÁC GẦN ĐÂY:
            {string.Join("\n", history)}

            CÂU HỎI:
            {question}

            Hướng dẫn trả lời:
            1. Trả lời bằng tiếng Việt, rõ ràng và chuyên nghiệp
            2. Nếu cần, hãy đưa ra các ví dụ cụ thể
            3. Nếu thông tin không đủ, hãy đề xuất các thông tin cần bổ sung
            4. Ưu tiên các giải pháp thực tế và khả thi
            5. Nếu có nhiều phương án, hãy phân tích ưu nhược điểm của từng phương án
            6. Đảm bảo câu trả lời phù hợp với ngữ cảnh của toàn bộ cuộc hội thoại";

            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var geminiResponse = await _httpClientGemini.PostAsJsonAsync($"{_settings.BaseUrl}/models/gemini-2.0-flash:generateContent?key={_settings.ApiKey}", payload);
            geminiResponse.EnsureSuccessStatusCode();

            var jsonResponse = await geminiResponse.Content.ReadFromJsonAsync<JsonElement>();
            var answer = jsonResponse.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();

            if (answer == null)
            {
                throw new NotFoundException(BuildingBlocks.Message.NOT_FOUND);
            }

            // Lưu câu hỏi và câu trả lời vào database
            await _messageRepository.AddAsync(new Entities.Message
            {
                ConversationId = conversation.Id,
                ProjectId = projectId,
                Content = answer,
                IsAI = true
            }, CancellationToken.None);
            await _messageRepository.SaveChangeAsync(CancellationToken.None);
            await _conversationRepository.CommitTransactionAsync(transaction, CancellationToken.None);

            return new ApiResponse<string>() { Data = answer, Message = BuildingBlocks.Message.CREATE_SUCCESSFULLY };
        }

        public async Task<ApiResponse<List<Conversation>>> GetConversationByProjectId(int projectId)
        {
            var conversationList = await _conversationRepository.GetAllQueryAble()
                .Include(e => e.Messages)
                .Where(e => e.ProjectId == projectId)
                .ToListAsync(CancellationToken.None);

            return new ApiResponse<List<Conversation>>() { Data = conversationList, Message = BuildingBlocks.Message.GET_SUCCESSFULLY };
        }
    }
}
