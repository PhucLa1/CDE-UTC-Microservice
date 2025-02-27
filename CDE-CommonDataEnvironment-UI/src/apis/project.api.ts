
import { Service } from "@/data/enums/service.enum";
import { Project } from "@/data/schema/Project/project.schema";
import { ProjectDetail } from "@/data/schema/Project/projectdetail.schema";
import { RoleDetail } from "@/data/schema/Project/role.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const projectApiRequest = {
    getList: () => http.get<ApiResponse<Project[]>>('projects', undefined, Service.ProjectService),
    create: (body: FormData) => http.post<ApiResponse<boolean>>('projects', body, undefined, Service.ProjectService),
    getDetail: (id: number) => http.get<ApiResponse<ProjectDetail>>('projects/' + id, undefined, Service.ProjectService),
    updateProject: (body: FormData) => http.put<ApiResponse<boolean>>('projects', body, undefined, Service.ProjectService),
    deleteProject: (id: number) => http.delete<ApiResponse<boolean>>('projects/' + id, null, undefined, Service.ProjectService),
    getRole: (id: number) => http.get<ApiResponse<RoleDetail>>('projects/' + id + '/role', undefined, Service.ProjectService),
}
export default projectApiRequest;