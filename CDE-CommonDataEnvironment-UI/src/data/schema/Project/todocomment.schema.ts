import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const todoCommentSchema = z.object({
    id: z.number().optional(),
    content: z
        .string()
        .min(1, "Nội dung dán phải có ít nhất 1 ký tự."),
    projectId: z.number().optional(),
    todoId: z.number().optional(),
})

export type TodoComment = z.infer<typeof todoCommentSchema>;
export const todoCommentDefault: TodoComment = {
    content: ""
};

