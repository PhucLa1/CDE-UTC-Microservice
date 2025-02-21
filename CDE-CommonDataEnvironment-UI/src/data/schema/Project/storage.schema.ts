import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const storageSchema = z.object({
    id: z.number().optional(),
    isFile: z.boolean().optional(),
    name: z.string().optional(),
    createdAt: z.string().optional(),
    createdBy: z.string().optional(),
    nameCreatedBy: z.string().optional(),
    projectId: z.number().optional(),
    parentId: z.number().optional(),
    tagNames: z.array(z.string()).optional(),
    urlImage: z.string().optional(),
})

export type Storage = z.infer<typeof storageSchema>;
export const storageDefault: Storage = {
};
