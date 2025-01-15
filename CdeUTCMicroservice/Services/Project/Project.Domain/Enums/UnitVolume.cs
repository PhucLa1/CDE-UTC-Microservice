using System.ComponentModel;

namespace Project.Domain.Enums
{
    public enum UnitVolume
    {
        [Description("Cubic Meters")]
        CubicMeters,
        [Description("Cubic Feet")]
        CubicFeet,
        [Description("Liters")]
        Liters,
        [Description("Gallons")]
        Gallons,
        // ... các đơn vị thể tích
    }

}
