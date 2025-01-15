using System.ComponentModel;

namespace Project.Domain.Enums
{
    public enum UnitLengthPrecision
    {
        [Description("Millimeters")]
        Millimeters,
        [Description("Centimeters")]
        Centimeters,
        [Description("Inches")]
        Inches,
        // ... độ chính xác chiều dài
    }
}
