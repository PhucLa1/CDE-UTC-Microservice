import { InvitationPermission } from "@/data/enums/invitationpermission.enum";
import { TodoVisibility } from "@/data/enums/todovisibility.enum";


export type Permission = {
    projectId?: number,
    todoVisibility: TodoVisibility,
    invitationPermission: InvitationPermission,
}