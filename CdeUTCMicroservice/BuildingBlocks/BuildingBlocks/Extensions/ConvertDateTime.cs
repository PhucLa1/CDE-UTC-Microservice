using System;
using BuildingBlocks.Enums;

namespace BuildingBlocks.Extensions
{
    public static class ConvertDateTime
    {
        public static string ConvertToFormat(this DateTime dateTime, DateDisplay dateDisplay, TimeDisplay timeDisplay)
        {
            string dateFormat = dateDisplay switch
            {
                DateDisplay.Iso8601 => "yyyy-MM-dd",
                DateDisplay.American => "MM/dd/yyyy",
                DateDisplay.British => "dd/MM/yyyy",
                DateDisplay.Vietnamese => "dd-MM-yyyy",
                DateDisplay.Short => "dd MMM yyyy",
                DateDisplay.Full => "dddd, dd MMMM yyyy",
                DateDisplay.Compact => "yy/MM/dd",
                _ => "yyyy-MM-dd" // Mặc định (ISO 8601)
            };

            string timeFormat = timeDisplay switch
            {
                TimeDisplay.TwelveHour => " hh:mm:ss tt",  // 12-hour format (AM/PM)
                TimeDisplay.TwentyFourHour => " HH:mm:ss", // 24-hour format
                TimeDisplay.Compact => " HH:mm",          // 24-hour format without seconds
                _ => " hh:mm:ss tt"
            };

            return dateTime.AddHours(7).ToString(dateFormat + timeFormat).Trim(); // Trim() để loại bỏ khoảng trắng nếu không có giờ
        }
    }
}
