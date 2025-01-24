import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const changePasswordSchema = z.object({
    email: z.string().optional(),
    password: z
        .string()
        .min(1, { message: "Email không được để trống" }),
        

    rePassword: z
        .string()
        .min(1, { message: "Mật khẩu không được để trống" }),
}).refine((data) => data.password === data.rePassword, {
    path: ["rePassword"],
    message: "Xác thực mật khẩu không đúng",
});

export type ChangePassword = z.infer<typeof changePasswordSchema>;
export const changePasswordDefault: ChangePassword = {
    rePassword: "",
    password: "",
};