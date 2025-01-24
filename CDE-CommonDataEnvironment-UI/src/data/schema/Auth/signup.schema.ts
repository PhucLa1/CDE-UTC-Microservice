import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const signUpSchema = z.object({
    email: z
        .string()
        .min(1, { message: "Email không được để trống" })
        .email({ message: "Email sai định dạng" }),

    mobilePhoneNumber: z
        .string()
        .regex(/^[0-9]{10,15}$/, {
            message: "Số điện thoại cá nhân sai định dạng",
        }),

    password: z
        .string()
        .min(1, { message: "Mật khẩu không được để trống" }),
    rePassword: z
        .string()
        .min(1, { message: "Vui lòng xác thực mật khẩu" }),

    firstName: z.string().min(1, { message: "Họ không được để trống" }),
    lastName: z.string().min(1, { message: "Tên không được để trống" }),
}).refine((data) => data.password === data.rePassword, {
    path: ["rePassword"],
    message: "Xác thực mật khẩu không đúng",
});

export type SignUp = z.infer<typeof signUpSchema>;
export const signUpDefault: SignUp = {
    email: "",
    firstName: "",
    lastName: "",
    password: "",
    rePassword: "",
    mobilePhoneNumber: ""
};