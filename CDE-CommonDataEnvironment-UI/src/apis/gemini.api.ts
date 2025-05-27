import { Service } from "@/data/enums/service.enum";
import { Conversation } from "@/data/schema/Project/conversation.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";
export interface GeminiAskRequest {
  projectId: number;
  conversationId?: number;
  question: string;
}

const geminiApiRequest = {
  getList: (projectId: number) =>
    http.get<ApiResponse<Conversation[]>>(
      "gemini/conversations/" + projectId,
      undefined,
      Service.AIService
    ),
  ask: (request: GeminiAskRequest) =>
    http.post<ApiResponse<string>>(
      "gemini",
      request,
      undefined,
      Service.AIService
    ),
};
export default geminiApiRequest;
