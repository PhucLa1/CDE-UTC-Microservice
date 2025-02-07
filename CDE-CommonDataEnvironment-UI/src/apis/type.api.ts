
import { Service } from "@/data/enums/service.enum";
import { Type } from "@/data/schema/Project/type.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const typeApiRequest = {
    create: (type: FormData) => http.post<ApiResponse<boolean>>('projects/type', type, undefined, Service.ProjectService),
    getList: (projectId: number) => http.get<ApiResponse<Type[]>>('projects/' + projectId + '/type', undefined, Service.ProjectService),
    delete: (type: Type) => http.delete<ApiResponse<boolean>>('projects/type', type, undefined, Service.ProjectService),
    update: (type: FormData) => http.put<ApiResponse<boolean>>('projects/type', type, undefined, Service.ProjectService)
}
export default typeApiRequest;