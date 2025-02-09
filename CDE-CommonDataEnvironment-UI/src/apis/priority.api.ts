
import { Service } from "@/data/enums/service.enum";
import { Priority } from "@/data/schema/Project/priority.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const priorityApiRequest = {
    create: (priority: Priority) => http.post<ApiResponse<boolean>>('projects/priority', priority, undefined, Service.ProjectService),
    getList: (projectId: number) => http.get<ApiResponse<Priority[]>>('projects/' + projectId + '/priority', undefined, Service.ProjectService),
    delete: (priority: Priority) => http.delete<ApiResponse<boolean>>('projects/priority', priority, undefined, Service.ProjectService),
    resset: (projectId: number) => http.delete<ApiResponse<boolean>>('projects/' + projectId + '/priority', null, undefined, Service.ProjectService),
    update: (priority: Priority) => http.put<ApiResponse<boolean>>('projects/priority', priority, undefined, Service.ProjectService)
}
export default priorityApiRequest;