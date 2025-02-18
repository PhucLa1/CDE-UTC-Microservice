using System.ComponentModel;

namespace Event.Core.Enums
{
    public enum ActivityTypeMode
    {
        [Description("Hoạt động xảy ra ngay lập tức")]
        Instant = 1,  
        [Description("Hoạt động được tổng hợp theo thời gian (ví dụ: báo cáo hàng ngày, hàng tuần)")]
        Digestly = 2  
    }
}
