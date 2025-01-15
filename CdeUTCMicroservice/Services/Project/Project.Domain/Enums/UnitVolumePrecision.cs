using System.ComponentModel;

namespace Project.Domain.Enums
{
    public enum UnitVolumePrecision
    {
        [Description("Cubic Millimeters")]
        CubicMillimeters,
        [Description("Milliliters")]
        Milliliters,
        [Description("Cubic Inches")]
        CubicInches,
        // ... độ chính xác thể tích
    }
}
