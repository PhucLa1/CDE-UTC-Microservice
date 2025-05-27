import { Service } from "@/data/enums/service.enum";
import { File } from "@/data/schema/Project/file.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const fileApiRequest = {
  create: (file: File) =>
    http.post<ApiResponse<boolean>>(
      "file",
      file,
      undefined,
      Service.ProjectService
    ),
  delete: (file: File) =>
    http.delete<ApiResponse<boolean>>(
      "file",
      file,
      undefined,
      Service.ProjectService
    ),
  getDetail: (id: number) =>
    http.get<ApiResponse<File>>(
      "file/" + id,
      undefined,
      Service.ProjectService
    ),
  update: (file: File) =>
    http.put<ApiResponse<boolean>>(
      "file",
      file,
      undefined,
      Service.ProjectService
    ),
  moveFile: (file: File) =>
    http.put<ApiResponse<boolean>>(
      "file/move-file",
      file,
      undefined,
      Service.ProjectService
    ),
};
export default fileApiRequest;
