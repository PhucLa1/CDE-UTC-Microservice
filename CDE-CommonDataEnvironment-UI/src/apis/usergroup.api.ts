import { Service } from "@/data/enums/service.enum";
import { AddUserGroupRequest, UserGroup } from "@/data/schema/Project/usergroup.schema";
import { UserProject } from "@/data/schema/Project/userproject.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const userGroupApiRequest = {
    getUsersByGroupId: (groupId: number) => http.get<ApiResponse<UserProject[]>>('group/' + groupId + '/user', undefined, Service.ProjectService),
    addUser: (userGroup: AddUserGroupRequest) => http.post<ApiResponse<boolean>>('group/user', userGroup, undefined, Service.ProjectService),
    deleteUser: (userGroup: UserGroup) => http.delete<ApiResponse<boolean>>('group/user', userGroup, undefined, Service.ProjectService),
}
export default userGroupApiRequest;