﻿namespace Project.Domain.Entities
{
    public class FileTag : BaseEntity
    {
        public int? FileId { get; set; } = default!;
        public int? TagId { get; set; } = default!;
        public File? File { get; set; }
        public Tag? Tag { get; set; }

    }
}
