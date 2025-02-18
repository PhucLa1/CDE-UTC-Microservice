import { InvitationPermission } from "@/data/enums/invitationpermission.enum"
import { Role } from "@/data/enums/role.enum"
import { TodoVisibility } from "@/data/enums/todovisibility.enum"

export type RoleDetail = {
    id: number,
    role: Role,
    invitationPermission: InvitationPermission,
    todoVisibility: TodoVisibility
}