import { InvitationPermission } from "@/data/enums/invitationpermission.enum"
import { Role } from "@/data/enums/role.enum"
import { TodoVisibility } from "@/data/enums/todovisibility.enum"

export type RoleDetail = {
    role: Role,
    invitationPermission: InvitationPermission,
    todoVisibility: TodoVisibility
}