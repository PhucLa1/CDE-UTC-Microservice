import { AnnotationAction } from "@/data/enums/annotationaction.enum";
import { z } from "zod";
import { tagSchema } from "./tag.schema";
import { userCommentSchema } from "./usercomment.schema";
export const annotationSchema = z.object({
    annotationAction: z.nativeEnum(AnnotationAction).optional(),
    action: z.nativeEnum(AnnotationAction).optional(),
    inkString: z.string().optional(),
    viewId: z.number().optional(),
    id: z.number().optional(),
})
export type Annotation = z.infer<typeof annotationSchema>;
export const viewSchema = z.object({
    id: z.number().optional(),
    name: z.string().min(1, "Nội dung dán phải có ít nhất 1 ký tự."),
    userId: z.number().optional(),
    description: z.string().min(1, "Nội dung dán phải có ít nhất 1 ký tự."),
    fileId: z.number().optional(),
    annotationModels: z.array(annotationSchema).optional(),
    createdAt: z.string().optional(),
    nameCreatedBy: z.string().optional(),
    tagNames: z.array(z.string()).optional(),
    thumbnailUrl: z.string().optional(),
    tagResults: z.array(tagSchema).optional(),
    userCommentResults: z.array(userCommentSchema).optional(),
    createdBy: z.number().optional(),
    projectId: z.number().optional(),
    tagIds: z.array(z.number()).optional(),
    url: z.string().optional(),
});

export type View = z.infer<typeof viewSchema>;

export const viewDefault: View = {
    name: "",
    description: "",
};

