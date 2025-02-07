
import { Service } from "@/data/enums/service.enum";
import { Status } from "@/data/schema/Project/status.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const statusApiRequest = {
    create: (status: Status) => http.post<ApiResponse<boolean>>('projects/status', status, undefined, Service.ProjectService),
    getList: (projectId: number) => http.get<ApiResponse<Status[]>>('projects/' + projectId + '/status', undefined, Service.ProjectService),
    delete: (status: Status) => http.delete<ApiResponse<boolean>>('projects/status', status, undefined, Service.ProjectService),
    update: (status: Status) => http.put<ApiResponse<boolean>>('projects/status', status, undefined, Service.ProjectService)
}
export default statusApiRequest;