import { z } from "zod";

export const fileHistorySchema = z.object({
    id: z.number().optional(),
    name: z.string().optional(),
    version: z.number().optional(),
    createdAt: z.string().optional(),
    createdBy: z.number().optional(),
    nameCreatedBy: z.string().optional(),
    imageUrl: z.string().optional(),
});

export type FileHistory = z.infer<typeof fileHistorySchema>;

export const filerHistoryDefault: FileHistory = {
};
