import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const prioritySchema = z.object({
    id: z.number().optional(),
    name: z
        .string()
        .min(1, "Tên nhãn dán phải có ít nhất 1 ký tự."),
    projectId: z.number().optional(),
    colorRGB: z.string().min(1, "Không được bỏ trống màu sắc."),
    isBlock: z.boolean().optional(),

})

export type Priority = z.infer<typeof prioritySchema>;
export const priorityDefault: Priority = {
    name: "",
    colorRGB: "#FFFFFF",
};
