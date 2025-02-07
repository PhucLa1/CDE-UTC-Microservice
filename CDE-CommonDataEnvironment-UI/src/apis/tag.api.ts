
import { Service } from "@/data/enums/service.enum";
import { DeleteTag, Tag } from "@/data/schema/Project/tag.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const tagApiRequest = {
    create: (tag: Tag) => http.post<ApiResponse<boolean>>('projects/tag', tag, undefined, Service.ProjectService),
    getList: (projectId: number) => http.get<ApiResponse<Tag[]>>('projects/' + projectId + '/tag', undefined, Service.ProjectService),
    delete: (deleteTags: DeleteTag) => http.delete<ApiResponse<boolean>>('projects/tag', deleteTags, undefined, Service.ProjectService),
    update: (tag: Tag) => http.put<ApiResponse<boolean>>('projects/tag', tag, undefined, Service.ProjectService)
}
export default tagApiRequest;