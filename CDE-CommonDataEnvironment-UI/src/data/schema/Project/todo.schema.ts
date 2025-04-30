import {  z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const todoSchema = z.object({
    id: z.number().optional(),
    name: z
        .string()
        .min(1, "Tên nhãn dán phải có ít nhất 1 ký tự."),
    projectId: z.number().optional(),
    typeId: z.number().optional(),
    statusId: z.number().optional(),
    priorityId: z.number().optional(),
    description: z.string().optional(),
    isAssignToGroup: z.number().optional(),
    dueDate: z.string().optional(),
    startDate: z.string().optional(),
    assignTo: z.number().optional(),
    fileIds: z.array(z.number()).optional(),
    viewIds: z.array(z.number()).optional(),
    tagIds: z.array(z.number()).optional(),
})

export type Todo = z.infer<typeof todoSchema>;
export const todoDefault: Todo = {
    name: ""
};
