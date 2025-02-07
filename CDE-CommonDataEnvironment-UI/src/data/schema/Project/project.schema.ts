import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const projectSchema = z.object({
    id: z.number().optional(),
    name: z
        .string()
        .min(1, "Tên dự án phải có ít nhất 1 ký tự."),
    startDate: z
        .string()
        .min(1, "Ngày bắt đầu là bắt buộc.")
        .refine((val) => new Date(val) > new Date(), {
            message: "Ngày bắt đầu phải lớn hơn ngày hiện tại.",
        }),
    endDate: z
        .string()
        .min(1, "Ngày kết thúc là bắt buộc.")
        .refine((val) => new Date(val) > new Date(), {
            message: "Ngày kết thúc phải lớn hơn ngày bắt đầu.",
        }),
    image: z
        .instanceof(File)
        .refine((file) => file.size > 0, {
            message: "Vui lòng chọn một ảnh.",
        }),
    imageUrl: z.string().optional(),
    description: z
        .string()
        .min(10, "Mô tả phải có ít nhất 10 ký tự.")
        .max(1000, "Mô tả không được vượt quá 1000 ký tự."),
}).refine((data) => new Date(data.endDate) > new Date(data.startDate), {
    path: ["endDate"],
    message: "Ngày kết thúc phải lớn hơn ngày bắt đầu",
});

export type Project = z.infer<typeof projectSchema>;
export const projectDefault: Project = {
    name: "",
    description: "",
    startDate: new Date().toISOString().split("T")[0], // Ngày mặc định là ngày hiện tại (dạng string)
    endDate: new Date(new Date().setDate(new Date().getDate() + 1)).toISOString().split("T")[0], // Ngày hôm sau (dạng string)
    image: new File([], "default.png"), // File ảnh mặc định (có thể thay đổi tùy ý)
};