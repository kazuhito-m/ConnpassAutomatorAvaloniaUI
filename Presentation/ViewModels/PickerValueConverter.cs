using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Presentation.ViewModels
{
    static class PickerValueConverter
    {
        private static readonly string DATE_FORMAT = "yyyy/MM/dd";

        public static string ToDateStringOf(DateTimeOffset? dateTimeOffset)
        {
            if (!dateTimeOffset.HasValue) return "";
            var date = dateTimeOffset.Value.Date;
            return date.ToString(DATE_FORMAT);
        }

        public static DateTimeOffset? ToDatePickerValueOf(string dateText)
        {
            if (dateText == null || dateText.Trim().Length == 0) return null;
            DateTime date;
            if (!DateTime.TryParseExact(dateText, DATE_FORMAT, null, DateTimeStyles.None, out date)) return null;
            return new DateTimeOffset(date);
        }

        public static string ToTimeStringOf(TimeSpan? timeSpan)
        {
            if (!timeSpan.HasValue) return "";
            var span = timeSpan.Value;
            var second = span.TotalSeconds;

            int hour = (int)second / 3600;
            int minute = (int)second % 3600 / 60;
            return string.Format("{0:00}:{1:00}", hour, minute);
        }

        public static TimeSpan? ToTimePickerValueOf(string timeText)
        {
            if (timeText == null || timeText.Trim().Length == 0) return null;
            var hourText = Regex.Replace(timeText, ":.*", "");
            var minuteText = Regex.Replace(timeText, ".*:", "");

            int hour;
            int minute;
            if (!int.TryParse(hourText, out hour) || !int.TryParse(minuteText, out minute)) return null;
            return TimeSpan.FromMinutes(hour * 60 + minute);
        }
    }
}
