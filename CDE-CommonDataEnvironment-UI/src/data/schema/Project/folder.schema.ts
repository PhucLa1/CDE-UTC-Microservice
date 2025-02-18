import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const folderSchema = z.object({
    id: z.number().optional(),
    name: z
        .string()
        .min(1, "Tên nhóm dán phải có ít nhất 1 ký tự."),
    projectId: z.number().optional(),
    parentId: z.number().optional()
})

export type Folder = z.infer<typeof folderSchema>;
export const folderDefault: Folder = {
    name: ""
};
