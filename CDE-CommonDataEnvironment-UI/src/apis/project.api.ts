import { Service } from "@/data/enums/service.enum";
import { Project } from "@/data/schema/Project/project.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const projectApiRequest = {
    getList: () => http.get<ApiResponse<Project[]>>('projects', undefined, Service.ProjectService),
    create: (body: FormData) => http.post<ApiResponse<boolean>>('projects', body, undefined, Service.ProjectService),
}
export default projectApiRequest;