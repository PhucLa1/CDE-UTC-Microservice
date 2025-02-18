
import { Service } from "@/data/enums/service.enum";
import { Folder } from "@/data/schema/Project/folder.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const folderApiRequest = {
    create: (folder: Folder) => http.post<ApiResponse<boolean>>('folder', folder, undefined, Service.ProjectService),
    delete: (folder: Folder) => http.delete<ApiResponse<boolean>>('folder', folder, undefined, Service.ProjectService),
    update: (folder: Folder) => http.put<ApiResponse<boolean>>('folder', folder, undefined, Service.ProjectService)
}
export default folderApiRequest;