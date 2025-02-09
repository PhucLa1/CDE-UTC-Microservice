
import { Service } from "@/data/enums/service.enum";
import { Permission } from "@/data/schema/Project/permission.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const permissionApiRequest = {
    get: (projectId: number) => http.get<ApiResponse<Permission>>('projects/' + projectId + '/permission', undefined, Service.ProjectService),
    update: (permission: Permission) => http.put<ApiResponse<boolean>>('projects/permission', permission, undefined, Service.ProjectService)
}
export default permissionApiRequest;