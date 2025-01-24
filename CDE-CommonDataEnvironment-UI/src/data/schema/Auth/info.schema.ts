import { DateDisplay } from "@/data/enums/datedisplay.enum";
import { TimeDisplay } from "@/data/enums/timedisplay.enum";
import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const infoSchema = z.object({
    email: z.string()
        .email("Invalid email format.")
        .optional()
        .refine((email) => email !== "", { message: "Email không được để trống." }),

    mobilePhoneNumber: z.string()
        .regex(/^\d{10}$/, "Định dạng số điện thoại sai.")
        .optional(),

    workPhoneNumber: z.string()
        .regex(/^\d{10}$/, "Định dạng số điện thoại sai.")
        .optional(),

    employer: z.string()
        .max(100, "Tên công việc không được vượt quá 100 kí tự")
        .optional(),

    imageUrl: z.string().optional(),
    dateDisplay: z.nativeEnum(DateDisplay).optional(),
    timeDisplay: z.nativeEnum(TimeDisplay).optional(),
    jobTitleId: z.string().optional(),
    firstName: z.string().optional(),
    lastName: z.string().optional(),
    cityId: z.string().optional(),
    districtId: z.string().optional(),
    wardId: z.string().optional()
});

export type Info = z.infer<typeof infoSchema>;
export const infoDefault: Info = {};