import { Service } from "@/data/enums/service.enum";
import { Annotation, View } from "@/data/schema/Project/view.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const viewApiRequest = {
    create: (view: View) => http.post<ApiResponse<boolean>>('view', view, undefined, Service.ProjectService),
    createAnnotation: (annotation: Annotation) => http.post<ApiResponse<boolean>>('view/add-annotation', annotation, undefined, Service.ProjectService),
    update: (view: View) => http.put<ApiResponse<boolean>>('view', view, undefined, Service.ProjectService),
    getList: (projectId: number) => http.get<ApiResponse<View[]>>('projects/' + projectId + '/view', undefined, Service.ProjectService),
    getById: (id: number) => http.get<ApiResponse<View>>('view/' + id , undefined, Service.ProjectService),
    getAnnotationByViewId: (viewId: number) => http.get<ApiResponse<Annotation[]>>('view/' + viewId + '/get-annotations' , undefined, Service.ProjectService),
    delete: (id: number) => http.delete<ApiResponse<boolean>>('view/' + id, undefined, undefined, Service.ProjectService),
}
export default viewApiRequest;