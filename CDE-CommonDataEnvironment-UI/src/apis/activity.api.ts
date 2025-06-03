import { Service } from "@/data/enums/service.enum";
import { TypeActivity } from "@/data/enums/typeactivity.enum";
import { Activity } from "@/data/schema/Project/activity,schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";
export interface GetActivityRequest {
  projectId: number;
  typeActivities?: TypeActivity[];
  startDate?: Date;
  endDate?: Date;
  createdBys?: number[];
}

const activityApiRequest = {
  getList: (request: string) =>
    http.get<ApiResponse<Activity[]>>(
      "activities?" + request,
      undefined,
      Service.EventService
    ),
};
export default activityApiRequest;
