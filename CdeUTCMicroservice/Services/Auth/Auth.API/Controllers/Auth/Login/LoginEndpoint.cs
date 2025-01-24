namespace Auth.API.Controllers.Auth.Login
{
    [Route(NameRouter.AUTH_ROUTER)]
    [ApiController]
    public class LoginEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.LOGIN)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await mediator.Send(loginRequest);
            SetJWT(result.Data);
            return Ok(result);
        }

        /// <summary>
        /// Đây là hàm lưu cookie vào client
        /// </summary>
        /// <param name="encryptedToken"></param>
        private void SetJWT(string encryptedToken)
        {
            HttpContext.Response.Cookies.Append("X-Access-Token", encryptedToken,
                 new CookieOptions
                 {
                     Expires = DateTime.UtcNow.AddDays(15),
                     HttpOnly = true,
                     Secure = true,
                     IsEssential = true,
                     SameSite = SameSiteMode.None
                 });
        }
    }
}
