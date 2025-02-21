
import { Service } from "@/data/enums/service.enum";
import { FileComment } from "@/data/schema/Project/filecomment.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const fileCommentApiRequest = {
    create: (file: FileComment) => http.post<ApiResponse<boolean>>('file-comment', file, undefined, Service.ProjectService),
    delete: (file: FileComment) => http.delete<ApiResponse<boolean>>('file-comment', file, undefined, Service.ProjectService),
    update: (file: FileComment) => http.put<ApiResponse<boolean>>('file-comment', file, undefined, Service.ProjectService),
}
export default fileCommentApiRequest;