import { z } from "zod";
import { messageSchema } from "./message.schema";
export const conversationSchema = z.object({
  id: z.number().optional(),
  title: z.string().optional(),
  projectId: z.number().optional(),
  isActive: z.boolean().optional(),
  context: z.string().optional(),
  messages: z.array(messageSchema).optional(),
  createdAt: z.string().optional(),
  updatedAt: z.string().optional(),
  createdBy: z.number().optional(),
  updatedBy: z.number().optional(),
});

export type Conversation = z.infer<typeof conversationSchema>;

export const conversationDefault: Conversation = {};
