import { TypeActivity } from "@/data/enums/typeactivity.enum";
import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const acvititySchema = z.object({
  id: z.number().optional(),
  action: z.string().optional(),
  resourceId: z.number().optional(),
  content: z.string().optional(),
  typeActivity: z.nativeEnum(TypeActivity).optional(),
  projectId: z.number().optional(),
  createdAt: z.string().optional(),
  fullName: z.string().optional(),
  userId: z.number().optional(),
  email: z.string().optional(),
  imageUrl: z.string().optional(),
});

export type Activity = z.infer<typeof acvititySchema>;
export const activityDefault: Activity = {};
