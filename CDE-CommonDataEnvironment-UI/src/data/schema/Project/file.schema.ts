import { z } from "zod";
import { userCommentSchema } from "./usercomment.schema";
import { tagSchema } from "./tag.schema";
import { fileHistorySchema } from "./filehistory.schema";
import { storagePermissionResultSchema } from "./storagepermisson.schema";
import { Permission } from "@/data/enums/permisson.enum";

export const fileSchema = z.object({
  id: z.number().optional(),
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
  fileHistoryResults: z.array(fileHistorySchema).optional(),
  thumbnail: z.string().optional(),
  storagePermissionResults: z.array(storagePermissionResultSchema).optional(),
  access: z.nativeEnum(Permission).optional(), // Thay đổi nếu có enum cụ thể
});

export type File = z.infer<typeof fileSchema>;
export const fileDefault: File = {
  name: "",
};


