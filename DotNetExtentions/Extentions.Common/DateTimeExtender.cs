using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extentions.Common
{
    public static class DateTimeExtender
    {
        public static DateTime GetDateForDayOfWeek(this DateTime dateInThisWeek, DayOfWeek dayOfWeek)
        {
            DateTime monday = dateInThisWeek.AddDays(-((int)dateInThisWeek.DayOfWeek - 1));
            DateTime result;

            if (dayOfWeek == DayOfWeek.Sunday)
            {
                result = monday.AddDays(6);
            }
            else
            {
                result = monday.AddDays((int)dayOfWeek - 1);
            }

            return result;
        }
    }
}
