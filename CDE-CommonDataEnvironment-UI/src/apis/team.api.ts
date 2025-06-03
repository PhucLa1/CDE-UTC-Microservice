import { Service } from "@/data/enums/service.enum";
import { UserProject } from "@/data/schema/Project/userproject.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const teamApiRequest = {
  leaveProject: (id: number) =>
    http.delete<ApiResponse<boolean>>(
      "team/leave-project/" + id,
      null,
      undefined,
      Service.ProjectService
    ),
  getUsersByProjectId: (projectId: number) =>
    http.get<ApiResponse<UserProject[]>>(
      "projects/" + projectId + "/team",
      undefined,
      Service.ProjectService
    ),
  GetAllTarget: (projectId: number) =>
    http.get<ApiResponse<TargetResult[]>>(
      "team/" + projectId + "/targets",
      undefined,
      Service.ProjectService
    ),
  inviteUser: (userProject: UserProject) =>
    http.post<ApiResponse<boolean>>(
      "team/invite-user",
      userProject,
      undefined,
      Service.ProjectService
    ),
  changeRole: (userProject: UserProject) =>
    http.put<ApiResponse<boolean>>(
      "team/change-role",
      userProject,
      undefined,
      Service.ProjectService
    ),
  kickUser: (userProject: UserProject) =>
    http.delete<ApiResponse<boolean>>(
      "team/kick-user",
      userProject,
      undefined,
      Service.ProjectService
    ),
};
export default teamApiRequest;

export interface TargetResult {
  id: number;
  name: string;
  isGroup: boolean;
  url: string;
  email: string;
}
