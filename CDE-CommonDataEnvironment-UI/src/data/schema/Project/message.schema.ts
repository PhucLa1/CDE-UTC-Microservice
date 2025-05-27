import { z } from "zod";

export const messageSchema = z.object({
  id: z.number().optional(),
  conversationId: z.number().optional(),
  projectId: z.number().optional(),
  isAI: z.boolean().optional(),
  content: z.string().optional(),
  context: z.string().optional(),
  conversation: z.any().optional(), // hoặc z.any().optional() nếu sau này sẽ có object
  createdAt: z.string().optional(), // ISO date string
  updatedAt: z.string().optional(), // ISO date string
  createdBy: z.number().optional(),
  updatedBy: z.number().optional(),
});

export type Message = z.infer<typeof messageSchema>;

export const messageDefault: Message = {};
