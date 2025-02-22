
import { Service } from "@/data/enums/service.enum";
import { PathItemResult, Storage } from "@/data/schema/Project/storage.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const storageApiRequest = {
    getList: (projectId: number, parentId: number) => http.get<ApiResponse<Storage[]>>(`storage/${projectId}/${parentId}`, undefined, Service.ProjectService),
    getFullPath: (folderId: number) => http.get<ApiResponse<PathItemResult[]>>(`storage/${folderId}/full-path`, undefined, Service.ProjectService),
}
export default storageApiRequest;