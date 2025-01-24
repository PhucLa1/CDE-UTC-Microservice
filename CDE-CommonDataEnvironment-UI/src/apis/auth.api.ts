import { Service } from "@/data/enums/service.enum";
import { ChangePassword } from "@/data/schema/Auth/changePassword.schema";
import { Info } from "@/data/schema/Auth/info.schema";
import { Login } from "@/data/schema/Auth/login.schema";
import { SendEmailVerify } from "@/data/schema/Auth/sendEmailVerify.schema";
import { SignUp } from "@/data/schema/Auth/signup.schema";
import { VerifyCode } from "@/data/schema/Auth/verifyCode.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const authApiRequest = {
    getInfoUser: () => http.get<ApiResponse<Info>>('auth/get-info', undefined, Service.AuthService),
    changeInfoUser: (data: FormData) => http.put<ApiResponse<boolean>>('auth/change-info', data, undefined, Service.AuthService),
    signUp: (signUp: SignUp) => http.post<ApiResponse<boolean>>('auth/sign-up', signUp, undefined, Service.AuthService),
    login: (login: Login) => http.post<ApiResponse<boolean>>('auth/login', login, undefined, Service.AuthService),
    sendEmailVerify: (sendEmailVerify: SendEmailVerify) => http.post<ApiResponse<boolean>>('auth/send-email-verify', sendEmailVerify, undefined, Service.AuthService),
    verifyCode: (verifyCode: VerifyCode) => http.put<ApiResponse<boolean>>('auth/verify-code', verifyCode, undefined, Service.AuthService),
    changePassword: (changePassword: ChangePassword) => http.put<ApiResponse<boolean>>('auth/change-password', changePassword, undefined, Service.AuthService),
}
export default authApiRequest;