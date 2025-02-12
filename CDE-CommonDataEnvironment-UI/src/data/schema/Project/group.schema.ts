import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const groupSchema = z.object({
    id: z.number().optional(),
    name: z
        .string()
        .min(1, "Tên nhóm dán phải có ít nhất 1 ký tự."),
    projectId: z.number().optional(),
    userCount: z.number().optional(),
})

export type Group = z.infer<typeof groupSchema>;
export const groupDefault: Group = {
    name: ""
};
