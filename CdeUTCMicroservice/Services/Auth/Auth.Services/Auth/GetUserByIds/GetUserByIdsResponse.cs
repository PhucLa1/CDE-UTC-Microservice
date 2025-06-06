﻿namespace Auth.Application.Auth.GetUserByIds
{
    public class GetUserByIdsResponse
    {
        public string FullName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
