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
        .refine((value) => value === '' || /^\d{10}$/.test(value), "Định dạng số điện thoại sai.")
        .optional()
        .nullable(),


    employer: z.string()
        .max(100, "Tên công việc không được vượt quá 100 kí tự")
        .optional(),

    imageUrl: z.string().nullable().optional(),
    dateDisplay: z.nativeEnum(DateDisplay).nullable().optional(),
    timeDisplay: z.nativeEnum(TimeDisplay).nullable().optional(),
    jobTitleId: z.number().nullable().optional(),
    firstName: z.string().optional(),
    lastName: z.string().optional(),
    cityId: z.number().nullable().optional(),
    districtId: z.number().nullable().optional(),
    wardId: z.number().nullable().optional()
});

export type Info = z.infer<typeof infoSchema>;
export const infoDefault: Info = {};