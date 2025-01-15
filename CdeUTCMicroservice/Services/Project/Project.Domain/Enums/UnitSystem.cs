using System.ComponentModel;

namespace Project.Domain.Enums
{
    public enum UnitSystem
    {
        [Description("Metric")] // Ví dụ mô tả
        Metric,
        [Description("Imperial")]
        Imperial
    }
}
