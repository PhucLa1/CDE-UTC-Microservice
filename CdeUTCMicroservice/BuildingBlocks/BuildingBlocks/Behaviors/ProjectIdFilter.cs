using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Behaviors
{
    public class ProjectIdFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Lấy projectId từ header
            var projectId = context.HttpContext.Request.Headers["X-Project-Id"].ToString();

            // Kiểm tra xem projectId có tồn tại và hợp lệ không
            if (string.IsNullOrEmpty(projectId))
            {
                /*
                context.Result = new BadRequestObjectResult(new
                {
                    Error = "ProjectId is required in header.",
                    Header = "X-Project-Id"
                });
                return;
                */
            }

            // (Tùy chọn) Lưu projectId vào HttpContext để sử dụng trong controller
            context.HttpContext.Items["ProjectId"] = projectId;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Không cần xử lý gì sau khi action chạy, để trống
        }
    }
}
