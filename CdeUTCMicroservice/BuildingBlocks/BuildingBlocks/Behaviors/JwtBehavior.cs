using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace BuildingBlocks.Behaviors
{
    public class JwtBehavior(RequestDelegate _next)
    {
        public async Task Invoke(HttpContext context)
        {

            var token = context.Request.Cookies["X-Access-Token"];

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                if (handler.CanReadToken(token))
                {
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                    // Lấy ra id của người dùng từ claim
                    var userId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;


                    // Lưu thông tin id, role của người dùng vào context để các middleware khác có thể sử dụng
                    context.Items["UserId"] = userId;
                }

            }

            await _next(context);
        }
    }
}
