import { Service } from "@/data/enums/service.enum";
import { Todo } from "@/data/schema/Project/todo.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const todoApiRequest = {
  create: (todo: Todo) =>
    http.post<ApiResponse<boolean>>(
      "todo",
      todo,
      undefined,
      Service.ProjectService
    ),
};
export default todoApiRequest;
