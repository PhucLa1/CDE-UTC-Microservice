import { TypeActivity } from "@/data/enums/typeactivity.enum";
import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const acvitityTypeSchema = z.object({
  id: z.number().optional(),
  typeActivity: z.nativeEnum(TypeActivity).optional(),
  projectId: z.number().optional(),
  isAcceptAll: z.boolean().optional(),
  timeSend: z.string().optional(),
  enabled: z.boolean().optional(),
  isUpdated: z.boolean().optional(),
  label: z.string().optional(),
  description: z.string().optional(),
});

export type ActivityType = z.infer<typeof acvitityTypeSchema>;
export interface UpdateActivityTypesDto {
  id: number;
  timeSend: string; // TimeSpan trong C# thường nên được biểu diễn dưới dạng "HH:mm:ss" hoặc "HH:mm" string
  enabled: boolean;
}

export interface UpdateActivityTypesRequest {
  updateActivityTypesDtos: UpdateActivityTypesDto[];
  isAcceptAll: boolean;
  projectId: number;
}

export const activityTypeDefault: ActivityType = {};
