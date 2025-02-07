import { Service } from "@/data/enums/service.enum";
import { Unit } from "@/data/schema/Project/unit.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const unitApiRequest = {
    getUnit: (projectId: number) => http.get<ApiResponse<Unit>>('projects/' + projectId + '/unit', undefined, Service.ProjectService),
    updateUnit: (unit: Unit) => http.put<ApiResponse<boolean>>('projects/unit', unit, undefined, Service.ProjectService),
}
export default unitApiRequest;