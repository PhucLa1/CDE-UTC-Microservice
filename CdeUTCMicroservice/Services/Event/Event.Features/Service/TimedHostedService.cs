using BuildingBlocks.Enums;
using ClosedXML.Excel;
using Event.Features.Features.Activities;
using Event.Infrastructure.Grpc;
using Event.Infrastructure.Grpc.GrpcRequest;

namespace Event.Features.Service
{
    public class TimedHostedService(
        IServiceScopeFactory scopeFactory,
        ILogger<TimedHostedService> logger
        ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Timed Hosted Service is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now.TimeOfDay;

                using var scope = scopeFactory.CreateScope();
                var baseRepository = scope.ServiceProvider.GetRequiredService<IBaseRepository<ActivityType>>();
                var activityTypes = await baseRepository
                   .GetAllQueryAble()
                   .Where(e => e.Enabled == true)
                   .ToListAsync(stoppingToken);

                var groupedByProject = activityTypes
                    .GroupBy(e => e.ProjectId)
                    .ToDictionary(
                        g => g.Key,
                        g => g.ToList()
                    );

                foreach (var activityType in groupedByProject)
                {
                    if (activityType.Value[0].IsAcceptAll)
                    {
                        var projectId = activityType.Value[0].ProjectId;
                        var runTime = activityType.Value[0].TimeSend;
                        var typeActivities = activityType.Value.Select(e => e.TypeActivity).ToList();
                        if (Math.Abs((now - runTime).TotalMinutes) < 1)
                        {
                            logger.LogInformation($"Running task at {DateTime.Now}");
                            await SendEmailAsync(projectId, typeActivities);
                        }
                    }
                    else
                    {
                        foreach (var sub in activityType.Value)
                        {
                            var runTime = sub.TimeSend;
                            var projectId = sub.ProjectId;
                            if (Math.Abs((now - runTime).TotalMinutes) < 1)
                            {
                                logger.LogInformation($"Running task at {DateTime.Now}");
                                await SendEmailAsync(projectId, new List<TypeActivity> { sub.TypeActivity });
                            }
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task SendEmailAsync(int projectId, List<TypeActivity> typeActivities)
        {
            using var scope = scopeFactory.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<TimedHostedService>>();
            var activityRepository = scope.ServiceProvider.GetRequiredService<IBaseRepository<Activity>>();
            var userGrpc = scope.ServiceProvider.GetRequiredService<IUserGrpc>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            logger.LogInformation(">> Đã thực hiện tác vụ theo giờ định sẵn.");

            var activities = await activityRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == projectId && typeActivities.Contains(e.TypeActivity))
                .ToListAsync(CancellationToken.None);

            var createdBys = activities.Select(e => e.CreatedBy).Distinct().ToList();
            var usersList = await userGrpc.GetUsersByIds(new GetUserRequestGrpc { Ids = createdBys });

            var result = (from a in activities
                          join u in usersList on a.CreatedBy equals u.Id
                          orderby a.Id descending
                          select new GetActivitiesResponse
                          {
                              Id = a.Id,
                              Action = a.Action,
                              ResourceId = a.ResourceId,
                              Content = a.Content,
                              TypeActivity = a.TypeActivity,
                              ProjectId = a.ProjectId,
                              CreatedAt = a.CreatedAt.ToString(),
                              FullName = u.FullName,
                              UserId = u.Id,
                              Email = u.Email,
                              ImageUrl = u.ImageUrl
                          }).ToList();

            var excelBytes = ExportActivitiesToExcel(result);
            var fileName = $"hoat-dong-{DateTime.Now:yyyyMMdd}.xlsx";
            var filePath = Path.Combine("wwwroot/Activity", fileName);
            await File.WriteAllBytesAsync(filePath, excelBytes);
            var downloadUrl = $"{Setting.EVENT_HOST}/Activity/{fileName}";

            var bodyContentEmail = HandleFile.READ_FILE("Email", "LogActivity.html")
                .Replace("{fileDownloadUrl}", downloadUrl);

            var contentEmail = emailService.TemplateContent.Replace("{content}", bodyContentEmail);


            RecipentEmail email = new()
            {
                To = "phucminhbeo@gmail.com",
                Body = contentEmail,
                Subject = "CDE - Thư thông báo hoạt động hàng ngày"
            };
            await emailService.SendEmailToRecipient(email);
        }

        public byte[] ExportActivitiesToExcel(List<GetActivitiesResponse> activities)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Hoạt động hôm nay");

            worksheet.Cell(1, 1).Value = "STT";
            worksheet.Cell(1, 2).Value = "Tên hoạt động";
            worksheet.Cell(1, 3).Value = "Thời gian";
            worksheet.Cell(1, 4).Value = "Người thực hiện";

            for (int i = 0; i < activities.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = i + 1;
                worksheet.Cell(i + 2, 2).Value = activities[i].Content;
                worksheet.Cell(i + 2, 3).Value = activities[i].CreatedAt;
                worksheet.Cell(i + 2, 4).Value = activities[i].Email;
            }

            worksheet.Columns().AdjustToContents();
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }

}
