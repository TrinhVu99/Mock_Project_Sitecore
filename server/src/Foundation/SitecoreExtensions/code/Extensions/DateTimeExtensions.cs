using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToClientDateTime(this DateTime datetime)
        {
            int timeZoneOffset = 0;
            if (HttpContext.Current?.Request?.Cookies["timezoneOffset"] != null)
            {
                timeZoneOffset = Int32.Parse(HttpContext.Current.Request.Cookies["timezoneOffset"].Value);
            }
            return datetime.ToUniversalTime().AddMinutes(timeZoneOffset);
        }
        public static string ToStringCulture(this DateTime datetime, string format, CultureInfo culture)
        {
            try
            {
                if(string.IsNullOrEmpty(format))
                {
                    return datetime.ToString();
                }
                if (culture == null)
                {
                    return datetime.ToString(format);
                }
                if (culture.Name.Equals("th-TH", StringComparison.OrdinalIgnoreCase))
                {
                    return FormatThaiDate(datetime, format, culture);
                }
                return datetime.ToString(format, culture);
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
        private static string FormatThaiDate(DateTime datetime, string format, CultureInfo culture)
        {
            int startIndex = format.IndexOf("y", StringComparison.OrdinalIgnoreCase);
            int lastIndex = format.LastIndexOf("y", StringComparison.OrdinalIgnoreCase);
            if (startIndex < 0 || lastIndex < 0)
            {
                return datetime.ToString(format, culture);
            }
            var yearFormat = format.Substring(startIndex, lastIndex - startIndex + 1);
            var temp = datetime.ToString(format.Replace(yearFormat, "{0}"), culture);
            var yearStr = yearFormat.Length <= 1 ? datetime.Year.ToString() : datetime.ToString(yearFormat);
            return string.Format(temp, yearStr);
        }
    }
}
