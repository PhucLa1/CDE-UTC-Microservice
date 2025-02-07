import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const statusSchema = z.object({
    id: z.number().optional(),
    name: z
        .string()
        .min(1, "Tên nhãn dán phải có ít nhất 1 ký tự."),
    projectId: z.number().optional(),
    colorRGB: z.string().min(1, "Không được bỏ trống màu sắc."),
    isDefault: z.boolean(),
    isBlock: z.boolean().optional(),

})

export type Status = z.infer<typeof statusSchema>;
export const statusDefault: Status = {
    name: "",
    isDefault: false,
    colorRGB: "#FFFFFF",
};
