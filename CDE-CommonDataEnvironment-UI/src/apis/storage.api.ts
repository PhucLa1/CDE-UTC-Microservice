
import { Service } from "@/data/enums/service.enum";
import { PathItemResult, Storage, UpdateStoragePermissionRequest } from "@/data/schema/Project/storage.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";
import { FolderItem } from "@/lib/zipUtil";

const storageApiRequest = {
    getList: (projectId: number, parentId: number) => http.get<ApiResponse<Storage[]>>(`storage/${projectId}/${parentId}`, undefined, Service.ProjectService),
    getFullPath: (folderId: number) => http.get<ApiResponse<PathItemResult[]>>(`storage/${folderId}/full-path`, undefined, Service.ProjectService),
    getTreeStorage: (id: number) => http.get<ApiResponse<FolderItem>>(`storage/tree-storage/${id}`, undefined, Service.ProjectService),
    updateStoragePermission: (updateStorageRequest: UpdateStoragePermissionRequest) => http.put<ApiResponse<boolean>>(`storage/update-permission`, updateStorageRequest, undefined, Service.ProjectService)
}
export default storageApiRequest;