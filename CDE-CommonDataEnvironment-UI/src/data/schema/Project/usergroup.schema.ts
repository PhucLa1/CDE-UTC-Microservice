import { z } from "zod";
import { Role } from "@/data/enums/role.enum";
import { UserProjectStatus } from "@/data/enums/userprojectstatus.enum";

export const userGroupSchema = z.object({
    id: z.number().optional(),
    projectId: z.number().optional(),
    userId: z.number().optional(),
    groupId: z.number().optional(),
    fullName: z.string().optional(),
    email: z.string().email("Email không hợp lệ."),
    imageUrl: z.string().optional(),
    dateJoined: z.string().optional(), // Nếu cần validate ngày, có thể dùng regex hoặc thư viện date
    userProjectStatus: z.nativeEnum(UserProjectStatus).optional(),
    role: z.nativeEnum(Role)
});

export type UserGroup = z.infer<typeof userGroupSchema>;

export const userGroupDefault: UserGroup = {
    email: "",
    role: Role.Admin, // Thay giá trị mặc định phù hợp
};

export type AddUserGroupRequest = {
    userIds: number[],
    groupId: number,
    projectId: number
}