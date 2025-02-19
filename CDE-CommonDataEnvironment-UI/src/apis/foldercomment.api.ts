
import { Service } from "@/data/enums/service.enum";
import { UserComment } from "@/data/schema/Project/usercomment.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const folderCommentApiRequest = {
    create: (folder: UserComment) => http.post<ApiResponse<boolean>>('folder-comment', folder, undefined, Service.ProjectService),
    delete: (folder: UserComment) => http.delete<ApiResponse<boolean>>('folder-comment', folder, undefined, Service.ProjectService),
    update: (folder: UserComment) => http.put<ApiResponse<boolean>>('folder-comment', folder, undefined, Service.ProjectService),
}
export default folderCommentApiRequest;