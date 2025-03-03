import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const viewCommentSchema = z.object({
    id: z.number().optional(),
    content: z
        .string()
        .min(1, "Nội dung dán phải có ít nhất 1 ký tự."),
    projectId: z.number().optional(),
    viewId: z.number().optional(),
})

export type ViewComment = z.infer<typeof viewCommentSchema>;
export const viewCommentDefault: ViewComment = {
    content: ""
};

