import { Service } from "@/data/enums/service.enum";
import { Info } from "@/data/schema/Auth/info.schema";
import { SignUp } from "@/data/schema/Auth/signup.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const authApiRequest = {
    getInfoUser: () => http.get<ApiResponse<Info>>('auth/get-info', undefined, Service.AuthService),
    changeInfoUser: (data: FormData) => http.put<ApiResponse<boolean>>('auth/change-info', data, undefined, Service.AuthService),
    signUp: (signUp: SignUp) => http.post<ApiResponse<boolean>>('auth/sign-up', signUp, undefined, Service.AuthService)

}
export default authApiRequest;