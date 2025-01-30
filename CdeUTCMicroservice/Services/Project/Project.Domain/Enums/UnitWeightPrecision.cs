using System.ComponentModel;

namespace Project.Domain.Enums
{
    public enum UnitWeightPrecision
    {
        [Description("0")]
        Zero,
        [Description("One-tenth (0.1)")]
        OneTenth,

        [Description("One-hundredth (0.01)")]
        OneHundredth,

        [Description("One-thousandth (0.001)")]
        OneThousandth
    }
}
