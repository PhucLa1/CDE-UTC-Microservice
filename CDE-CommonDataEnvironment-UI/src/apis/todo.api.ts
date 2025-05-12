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
    delete: (id: number) =>
      http.delete<ApiResponse<boolean>>(
        "todo/" + id,
        undefined,
        undefined,
        Service.ProjectService
      ),
    update: (id: number, todo: Todo) => http.put<ApiResponse<boolean>>('todo/' + id, todo, undefined, Service.ProjectService),
    getList: (projectId: number) => 
      http.get<ApiResponse<Todo[]>>(
       "todo/" + projectId ,
       undefined,
       Service.ProjectService
      )
};
export default todoApiRequest;
