import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const userCommentSchema = z.object({
    id: z.number().optional(),
    email: z.string().optional(),
    name: z.string().optional(),
    avatarUrl: z.string().optional(),
    content: z.string().min(1, "Nội dung phải có ít nhất 1 ký tự."),
    updatedBy: z.number().optional(),
    updatedAt: z.string().optional()
})

export type UserComment = z.infer<typeof userCommentSchema>;
export const userCommentDefault: UserComment = {
    content: ""
};