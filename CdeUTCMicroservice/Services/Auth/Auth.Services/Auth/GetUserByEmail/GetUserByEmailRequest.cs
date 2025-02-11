namespace Auth.Application.Auth.GetUserByEmail
{
    public class GetUserByEmailRequest : IQuery<ApiResponse<GetUserByEmailResponse>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
