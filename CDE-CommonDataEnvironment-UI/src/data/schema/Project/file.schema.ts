import { z } from "zod";
import { userCommentSchema } from "./usercomment.schema";
import { tagSchema } from "./tag.schema";
import { fileHistorySchema } from "./filehistory.schema";

export const fileSchema = z.object({
    name: z.string().min(1, "Tên không được để trống"),
    size: z.number().optional(),
    url: z.string().optional(),
    folderId: z.number().int().optional(),
    projectId: z.number().int().optional(),
    fileVersion: z.number().int().optional(),
    fileType: z.any().optional(), // Thay đổi nếu có enum cụ thể
    mimeType: z.string().optional(),
    extension: z.string().optional(),
    nameCreatedBy: z.string().optional(),
    userCommentResults: z.array(userCommentSchema).optional(),
    tagResults: z.array(tagSchema).optional(),
    createdBy: z.number().optional(),
    tagIds: z.array(z.number()).optional(),
    createdAt: z.string().optional(),
    fileHistoryResults: z.array(fileHistorySchema).optional()
});

export type File = z.infer<typeof fileSchema>;
export const fileDefault: File = {
    name: ""
};