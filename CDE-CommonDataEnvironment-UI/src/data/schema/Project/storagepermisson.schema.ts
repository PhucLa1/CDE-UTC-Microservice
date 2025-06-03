import { z } from "zod";

export const storagePermissionResultSchema = z.object({
  id: z.number().optional(),
  name: z.string(),
  email: z.string(),
  targetId: z.number(),
  access: z.number(),
  url: z.string().optional(),
});

export type StoragePermissionResult = z.infer<
  typeof storagePermissionResultSchema
>;
