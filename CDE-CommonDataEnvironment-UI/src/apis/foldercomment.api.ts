
import { Service } from "@/data/enums/service.enum";
import { FolderComment } from "@/data/schema/Project/foldercomment.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const folderCommentApiRequest = {
    create: (folder: FolderComment) => http.post<ApiResponse<boolean>>('folder-comment', folder, undefined, Service.ProjectService),
    delete: (folder: FolderComment) => http.delete<ApiResponse<boolean>>('folder-comment', folder, undefined, Service.ProjectService),
    update: (folder: FolderComment) => http.put<ApiResponse<boolean>>('folder-comment', folder, undefined, Service.ProjectService),
}
export default folderCommentApiRequest;