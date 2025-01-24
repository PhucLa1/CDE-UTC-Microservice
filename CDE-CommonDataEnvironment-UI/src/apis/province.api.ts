
import { Service } from "@/data/enums/service.enum";
import { Province } from "@/data/schema/Auth/province.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const provinceApiRequest = {
    getAllProvince: (stringAdd: string) => http.get<ApiResponse<Province[]>>(`provinces${stringAdd}`, undefined, Service.AuthService)
}
export default provinceApiRequest;