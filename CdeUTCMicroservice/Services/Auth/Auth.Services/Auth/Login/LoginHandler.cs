



namespace Auth.Application.Auth.Login
{
    internal class LoginHandler
        (IBaseRepository<User> userRepository, IOptions<JwtSetting> jwtServerSetting)
        : ICommandHandler<LoginRequest, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginRequest command, CancellationToken cancellationToken)
        {
            var user = await userRepository
                .GetAllQueryAble()
                .Where(e => e.Email == command.Email)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
                throw new BadRequestException("Email không tồn tại");


            bool isCorrectPass = BCrypt.Net.BCrypt.Verify(command.Password, user.Password);
            if (!isCorrectPass)
                throw new BadRequestException("Mật khẩu không đúng!");

            string encrypterToken = JWTGenerator(user);
            Console.WriteLine(encrypterToken);
            return new LoginResponse { Data = encrypterToken, Message = Message.LOGIN_SUCCESSFULLY };



        }


        private string JWTGenerator(User user)
        {
            try
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Name, jwtServerSetting.Value.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Email", user.Email),
                        new Claim("Password", user.Password),
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtServerSetting.Value.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    jwtServerSetting.Value.Issuer,
                    jwtServerSetting.Value.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: signIn);

                var encrypterToken = new JwtSecurityTokenHandler().WriteToken(token);
                return encrypterToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
