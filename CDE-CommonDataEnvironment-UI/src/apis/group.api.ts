
import { Service } from "@/data/enums/service.enum";
import { Group } from "@/data/schema/Project/group.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const groupApiRequest = {
    create: (group: Group) => http.post<ApiResponse<boolean>>('group', group, undefined, Service.ProjectService),
    getList: (projectId: number) => http.get<ApiResponse<Group[]>>('projects/' + projectId + '/group', undefined, Service.ProjectService),
    delete: (group: Group) => http.delete<ApiResponse<boolean>>('group', group, undefined, Service.ProjectService),
    update: (group: Group) => http.put<ApiResponse<boolean>>('group', group, undefined, Service.ProjectService)
}
export default groupApiRequest;