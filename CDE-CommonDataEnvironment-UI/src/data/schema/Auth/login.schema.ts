import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const loginSchema = z.object({
    email: z
        .string()
        .min(1, { message: "Email không được để trống" })
        .email({ message: "Email sai định dạng" }),

    password: z
        .string()
        .min(1, { message: "Mật khẩu không được để trống" }),
})

export type Login = z.infer<typeof loginSchema>;
export const loginDefault: Login = {
    email: "",
    password: "",
};