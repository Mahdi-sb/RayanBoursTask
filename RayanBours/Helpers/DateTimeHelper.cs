using System.Globalization;

namespace RayanBours.Helpers
{
    public static class DateTimeHelper
    {
        public static string ToPersianDate(this DateTime value)
        {
            return value.ToString("yyyy/MM/dd", new CultureInfo("fa-IR"));
        }
        public static DateTime ToGregorianDate(this string value)
        {
            try
            {
                var date = value.Split("/");
                var persianCalendar = new PersianCalendar();
                var gregorianDateTime = persianCalendar.ToDateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]),
                    Convert.ToInt32(date[2]), 0, 0, 0, 0);
                return gregorianDateTime;
            }
            catch (Exception e)
            {

                throw new Exception("تاریخ وارد شده معتبر نمی باشد");
            }

        }
        public static bool IsGregorianDate(this string value)
        {
            try
            {
                var date = value.Split("/");
                var persianCalendar = new PersianCalendar();
                var gregorianDateTime = persianCalendar.ToDateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]),
                    Convert.ToInt32(date[2]), 0, 0, 0, 0);
                return true;
            }
            catch (Exception e)
            {

                return false;
            }

        }
    }
}