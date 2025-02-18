
import { Service } from "@/data/enums/service.enum";
import { Storage } from "@/data/schema/Project/storage.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const storageApiRequest = {
    getList: (projectId: number, parentId: number) => http.get<ApiResponse<Storage[]>>(`storage/${projectId}/${parentId}`, undefined, Service.ProjectService),
}
export default storageApiRequest;