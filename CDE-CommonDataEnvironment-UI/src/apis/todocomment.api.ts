
import { Service } from "@/data/enums/service.enum";
import { TodoComment } from "@/data/schema/Project/todocomment.schema";
import { UserComment } from "@/data/schema/Project/usercomment.schema";
import { ApiResponse } from "@/data/type/response.type";
import http from "@/lib/http";

const todoCommentApiRequest = {
    create: (todo: TodoComment) => http.post<ApiResponse<boolean>>('todo-comment', todo, undefined, Service.ProjectService),
    delete: (id: number) => http.delete<ApiResponse<boolean>>('todo-comment/' + id, undefined, undefined, Service.ProjectService),
    update: (id: number, todo: TodoComment) => http.put<ApiResponse<boolean>>('todo-comment/' + id, todo, undefined, Service.ProjectService),
    getListByTodoId: (id: number) => http.get<ApiResponse<UserComment[]>>('todo-comment/' + id, undefined, Service.ProjectService),
}
export default todoCommentApiRequest;