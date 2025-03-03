import { Service } from "@/data/enums/service.enum";
import { View } from "@/data/schema/Project/view.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const viewApiRequest = {
    create: (view: View) => http.post<ApiResponse<boolean>>('view', view, undefined, Service.ProjectService),
    getList: (projectId: number) => http.get<ApiResponse<View[]>>('projects/' + projectId + '/view', undefined, Service.ProjectService),
    getById: (id: number) => http.get<ApiResponse<View>>('view/' + id , undefined, Service.ProjectService)
}
export default viewApiRequest;