import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const verifyCodeSchema = z.object({
    email: z
        .string()
        .email({ message: "Email sai định dạng" })
        .optional(),
    code: z.string().min(6, {
        message: "Mã code phải bao gồm 6 chữ số ",
      })
})

export type VerifyCode = z.infer<typeof verifyCodeSchema>;
export const verifyCodeDefault: VerifyCode = {
    code: "",
};