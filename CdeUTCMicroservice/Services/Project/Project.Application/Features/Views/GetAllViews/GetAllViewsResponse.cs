﻿namespace Project.Application.Features.Views.GetAllViews
{
    public class GetAllViewsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public string NameCreatedBy { get; set; } = string.Empty;
        public List<string> TagNames { get; set; } = new List<string>() { };
    }
}
