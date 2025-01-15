﻿namespace Event.Core.Entities.Base
{
    public interface IAuditable
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        Guid CreatedBy { get; set; }
        Guid UpdatedBy { get; set; }
    }
}
