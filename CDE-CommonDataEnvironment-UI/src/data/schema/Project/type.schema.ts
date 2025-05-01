import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const typeSchema = z.object({
    id: z.number().optional(),
    name: z
        .string()
        .min(1, "Tên nhãn dán phải có ít nhất 1 ký tự."),
    projectId: z.number().optional(),
    imageIconUrl: z.string().optional(),
    iconImage: z
        .instanceof(File),
    isBlock: z.boolean().optional(),
    iconImageUrl: z.string().optional(),

}).refine((data) => data.id || (data.iconImage && data.iconImage.size > 0), {
    path: ["iconImage"],
    message: "Vui lòng chọn ảnh",
});

export type Type = z.infer<typeof typeSchema>;
export const typeDefault: Type = {
    name: "",
    iconImage: new File([], "default.png"),
};
