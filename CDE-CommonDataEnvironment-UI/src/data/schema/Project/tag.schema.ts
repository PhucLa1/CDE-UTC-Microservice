import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const tagSchema = z.object({
    id: z.number().optional(),
    name: z
        .string()
        .min(1, "Tên nhãn dán phải có ít nhất 1 ký tự."),
    projectId: z.number().optional(),
    isBlock: z.boolean().optional(),
})

export type Tag = z.infer<typeof tagSchema>;
export const tagDefault: Tag = {
    name: ""
};

export type DeleteTag = {
    ids: number[],
    projectId: number
}