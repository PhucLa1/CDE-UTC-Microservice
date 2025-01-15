using System.ComponentModel;

namespace Project.Domain.Enums
{
    public enum ActivityType
    {
        [Description("Digest")]
        Digest,
        [Description("Instant")]
        Instant,
        // ... các loại hoạt động
    }
}
