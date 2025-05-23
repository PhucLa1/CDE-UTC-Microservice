﻿namespace Project.Domain.Entities
{
    public class Status : BaseEntity
    {
        public int? ProjectId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public string ColorRGB { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool IsBlock { get; set; } = false;
        public Projects? Project { get; set; }


        public static List<Status> InitData(int projectId)
        {
            return new List<Status>()
            {
                new Status() { ProjectId = projectId, IsDefault = true, IsActive = true, ColorRGB = "#1E7BD7", Name = "Mới", IsBlock = true},
                new Status() { ProjectId = projectId, IsDefault = false, IsActive = true, ColorRGB = "#28A745", Name = "Đang thực hiện", IsBlock = true },
                new Status() { ProjectId = projectId, IsDefault = false, IsActive = true, ColorRGB = "#FFC107", Name = "Đang chờ" , IsBlock = true},
                new Status() { ProjectId = projectId, IsDefault = false, IsActive = true, ColorRGB = "#DC3545", Name = "Hoàn thành", IsBlock = true },
                new Status() { ProjectId = projectId, IsDefault = false, IsActive = true, ColorRGB = "#6C757D", Name = "Đã đóng" , IsBlock = true},
            };
        }
    }
}
