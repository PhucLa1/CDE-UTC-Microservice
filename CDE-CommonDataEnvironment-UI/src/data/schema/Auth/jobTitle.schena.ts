import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const jobTitleSchema = z.object({
    name: z.string().optional(),
    uuid: z.string().optional(),
    id: z.number().optional()

});

export type JobTitle = z.infer<typeof jobTitleSchema>;
export const jobTitleDefault: JobTitle = {};