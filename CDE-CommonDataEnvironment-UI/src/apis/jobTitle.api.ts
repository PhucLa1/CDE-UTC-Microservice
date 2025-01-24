import { Service } from "@/data/enums/service.enum";
import { JobTitle } from "@/data/schema/Auth/jobTitle.schena";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const jobTitleApiRequest = {
    getAllJobTitle: () => http.get<ApiResponse<JobTitle[]>>('job-titles', undefined, Service.AuthService),

}
export default jobTitleApiRequest;