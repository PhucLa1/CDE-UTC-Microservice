import { string, z } from "zod";
import { userCommentSchema } from "./usercomment.schema";
import { tagSchema } from "./tag.schema";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const folderSchema = z.object({
    id: z.number().optional(),
    name: z
        .string()
        .min(1, "Tên nhóm dán phải có ít nhất 1 ký tự."),
    projectId: z.number().optional(),
    parentId: z.number().optional(),
    createdAt: z.string().optional(),
    nameCreatedBy: z.string().optional(),
    userCommentResults: z.array(userCommentSchema).optional(),
    tagResults: z.array(tagSchema).optional(),
    createdBy: z.number().optional(),
    tagIds: z.array(z.number()).optional()
})

export type Folder = z.infer<typeof folderSchema>;
export const folderDefault: Folder = {
    name: ""
};

