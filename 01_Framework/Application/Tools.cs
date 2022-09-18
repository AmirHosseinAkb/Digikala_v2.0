using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application
{
    public static class Tools
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar persian = new PersianCalendar();
            return persian.GetYear(value) + "/" + persian.GetMonth(value).ToString("00") + "/" +
                   persian.GetDayOfMonth(value).ToString("00");

        }

        public static DateTime ShamsiToGerogorian(this string value)
        {
            var date = value.Split('/');
            return new DateTime(int.Parse(date[0])
                , int.Parse(date[1]), int.Parse(date[2])
                , new PersianCalendar());
        }
    }
}
