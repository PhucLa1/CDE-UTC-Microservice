﻿namespace Auth.Repositories.Setting
{
    public class JwtSetting
    {
        public required string Key { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string Subject { get; set; }
    }
}
