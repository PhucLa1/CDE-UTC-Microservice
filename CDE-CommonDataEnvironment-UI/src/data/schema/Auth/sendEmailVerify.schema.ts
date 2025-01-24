import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const sendEmailVerifySchema = z.object({
    email: z
        .string()
        .min(1, { message: "Email không được để trống" })
        .email({ message: "Email sai định dạng" }),
})

export type SendEmailVerify = z.infer<typeof sendEmailVerifySchema>;
export const sendEmailVerifyDefault: SendEmailVerify = {
    email: ""
};