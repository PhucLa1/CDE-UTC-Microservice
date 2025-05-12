import { ActivityTypeSchemaWithIcon } from "@/app/project/[id]/notifications/page";
import { Service } from "@/data/enums/service.enum";
import { UpdateActivityTypesRequest } from "@/data/schema/Project/activitytype.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const activityTypeApiRequest = {
  getList: (projectId: number) =>
    http.get<ApiResponse<ActivityTypeSchemaWithIcon[]>>(
      "activities-type?projectId=" + projectId,
      undefined,
      Service.EventService
    ),
  update: (request: UpdateActivityTypesRequest) =>
    http.put<ApiResponse<boolean>>(
      "activities-type",
      request,
      undefined,
      Service.EventService
    ),
};
export default activityTypeApiRequest;
