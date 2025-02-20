import { z } from "zod";

export const folderHistorySchema = z.object({
    id: z.number().optional(),
    name: z.string().optional(),
    version: z.number().optional(),
    createdAt: z.string().optional(),
    createdBy: z.number().optional(),
    nameCreatedBy: z.string().optional()
});

export type FolderHistory = z.infer<typeof folderHistorySchema>;

export const folderHistoryDefault: FolderHistory = {
};
