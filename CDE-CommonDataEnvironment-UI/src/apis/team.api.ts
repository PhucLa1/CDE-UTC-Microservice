import { Service } from "@/data/enums/service.enum";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const teamApiRequest = {
    leaveProject: (id: string) => http.delete<ApiResponse<boolean>>('team/leave-project/' + id, null, undefined, Service.ProjectService),
}
export default teamApiRequest;