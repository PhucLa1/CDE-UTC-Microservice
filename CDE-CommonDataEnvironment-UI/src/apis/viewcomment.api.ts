
import { Service } from "@/data/enums/service.enum";
import { ViewComment } from "@/data/schema/Project/viewcomment.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const fileCommentApiRequest = {
    create: (file: ViewComment) => http.post<ApiResponse<boolean>>('view-comment', file, undefined, Service.ProjectService),
    delete: (file: ViewComment) => http.delete<ApiResponse<boolean>>('view-comment', file, undefined, Service.ProjectService),
    update: (file: ViewComment) => http.put<ApiResponse<boolean>>('view-comment', file, undefined, Service.ProjectService),
}
export default fileCommentApiRequest;