using System.ComponentModel;

namespace Project.Domain.Enums
{
    public enum TodoVisibility
    {
        [Description("Mọi người đều có thể nhìn thấy todo")]
        Default,
        [Description("Todo chỉ có thể nhìn bời admin, người tạo todo, và người được giao")]
        Restricted
    }
}
