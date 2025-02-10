import { z } from "zod";
import { Role } from "@/data/enums/role.enum";
import { UserProjectStatus } from "@/data/enums/userprojectstatus.enum";

export const userProjectSchema = z.object({
    id: z.number(),
    fullName: z.string().min(1, "Tên người dùng phải có ít nhất 1 ký tự."),
    email: z.string().email("Email không hợp lệ."),
    imageUrl: z.string().url("URL ảnh không hợp lệ."),
    dateJoined: z.string(), // Nếu cần validate ngày, có thể dùng regex hoặc thư viện date
    userProjectStatus: z.nativeEnum(UserProjectStatus),
    role: z.nativeEnum(Role),
});

export type UserProject = z.infer<typeof userProjectSchema>;

export const userProjectDefault: UserProject = {
    id: 0,
    fullName: "",
    email: "",
    imageUrl: "",
    dateJoined: "",
    userProjectStatus: UserProjectStatus.Pending, // Thay giá trị mặc định phù hợp
    role: Role.Member, // Thay giá trị mặc định phù hợp
};