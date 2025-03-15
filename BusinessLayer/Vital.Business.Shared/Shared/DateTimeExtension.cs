using System;
using System.Diagnostics;

namespace Vital.Business.Shared.Shared
{
    public static class DateTimeExtension
    {
        #region Private Variables

        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

        #endregion

        #region Extensions

        /// <summary>
        /// Check if the date time is valid.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool IsValid(this DateTime target)
        {
            return (target >= MinDate) && (target <= MaxDate);
        }

        /// <summary>
        /// Gets a DateTime representing the first day in the current month
        /// </summary>
        /// <param name="current">The current date</param>
        /// <returns></returns>
        public static DateTime First(this DateTime current)
        {
            var first = current.AddDays(1 - current.Day);
            return first;
        }

        /// <summary>
        /// Gets a DateTime representing the first specified day in the current month
        /// </summary>
        /// <param name="current">The current day</param>
        /// <param name="dayOfWeek">The current day of week</param>
        /// <returns></returns>
        public static DateTime First(this DateTime current, DayOfWeek dayOfWeek)
        {
            var first = current.First();

            if (first.DayOfWeek != dayOfWeek)
            {
                first = first.Next(dayOfWeek);
            }

            return first;
        }

        /// <summary>
        /// Gets a DateTime representing the last day in the current month
        /// </summary>
        /// <param name="current">The current date</param>
        /// <returns></returns>
        public static DateTime Last(this DateTime current)
        {
            int daysInMonth = DateTime.DaysInMonth(current.Year, current.Month);

            var last = current.First().AddDays(daysInMonth - 1);
            return last;
        }

        /// <summary>
        /// Gets a DateTime representing the last specified day in the current month
        /// </summary>
        /// <param name="current">The current date</param>
        /// <param name="dayOfWeek">The current day of week</param>
        /// <returns></returns>
        public static DateTime Last(this DateTime current, DayOfWeek dayOfWeek)
        {
            var last = current.Last();

            last = last.AddDays(Math.Abs(dayOfWeek - last.DayOfWeek) * -1);
            return last;
        }

        /// <summary>
        /// Gets a DateTime representing the first date following the current date which falls on the given day of the week
        /// </summary>
        /// <param name="current">The current date</param>
        /// <param name="dayOfWeek">The day of week for the next date to get</param>
        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - current.DayOfWeek;

            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }

            DateTime result = current.AddDays(offsetDays);
            return result;
        }

        /// <summary>
        /// Gets a DateTime representing midnight on the current date
        /// </summary>
        /// <param name="current">The current date</param>
        public static DateTime Midnight(this DateTime current)
        {
            var midnight = new DateTime(current.Year, current.Month, current.Day);
            return midnight;
        }

        /// <summary>
        /// Gets a DateTime representing noon on the current date
        /// </summary>
        /// <param name="current">The current date</param>
        public static DateTime Noon(this DateTime current)
        {
            var noon = new DateTime(current.Year, current.Month, current.Day, 12, 0, 0);
            return noon;
        }

        /// <summary>
        /// Sets the time of the current date with minute precision
        /// </summary>
        /// <param name="current">The current date</param>
        /// <param name="hour">The hour</param>
        /// <param name="minute">The minute</param>
        public static DateTime SetTime(this DateTime current, int hour, int minute)
        {
            return SetTime(current, hour, minute, 0, 0);
        }

        /// <summary>
        /// Sets the time of the current date with second precision
        /// </summary>
        /// <param name="current">The current date</param>
        /// <param name="hour">The hour</param>
        /// <param name="minute">The minute</param>
        /// <param name="second">The second</param>
        /// <returns></returns>
        public static DateTime SetTime(this DateTime current, int hour, int minute, int second)
        {
            return SetTime(current, hour, minute, second, 0);
        }

        /// <summary>
        /// Sets the time of the current date with millisecond precision
        /// </summary>
        /// <param name="current">The current date</param>
        /// <param name="hour">The hour</param>
        /// <param name="minute">The minute</param>
        /// <param name="second">The second</param>
        /// <param name="millisecond">The millisecond</param>
        /// <returns></returns>
        public static DateTime SetTime(this DateTime current, int hour, int minute, int second, int millisecond)
        {
            var atTime = new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);
            return atTime;
        }

        /// <summary>
        /// Sets the day for the given datetime.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="day">The day.</param>
        /// <returns></returns>
        public static DateTime SetDay(this DateTime current, int day)
        {
            var atDate = new DateTime(current.Year, current.Month, day);
            return atDate;
        }

        /// <summary>
        /// Get the months between the start and end date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public static int MonthsUntil(this DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }


        /// <summary>
        /// Gets the number of months.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        public static double GetNumberOfMonths(this DateTime fromDate, DateTime toDate)
        {
            Double returnValue;
            TimeSpan ts;
            if (fromDate.Month == toDate.Month)
            {
                //Within the same month do pro rata calculation
                ts = toDate.Subtract(fromDate.AddDays(-1));
                returnValue = Convert.ToDouble(ts.Days / DateTime.DaysInMonth(fromDate.Year, fromDate.Month));
            }
            else
            {
                //Check the first month
                if (fromDate.Day == 1)
                {
                    //Check the last month 
                    if (toDate.Day == DateTime.DaysInMonth(toDate.Year, toDate.Month))
                    {
                        //The whole last month is included
                        ts = toDate.Subtract(fromDate.AddDays(-1));
                        returnValue = Convert.ToDouble(((ts.Days) / 30).ToString().Split(".".ToCharArray())[0]);
                    }
                    else
                    {
                        //Only part of the last month is included
                        ts = toDate.Subtract(fromDate.AddDays(-1));
                        Double wholeMonth = Convert.ToDouble(((ts.Days) / 30).ToString().Split(".".ToCharArray())[0]);
                        ts = toDate.Subtract(new DateTime(toDate.Year, toDate.Month, 1).AddDays(-1));
                        returnValue = wholeMonth +
                                      Convert.ToDouble(ts.Days / DateTime.DaysInMonth(toDate.Year, toDate.Month));
                    }
                }
                else
                {
                    if (toDate.Day == DateTime.DaysInMonth(toDate.Year, toDate.Month))
                    {
                        //Only part of the first month is included
                        ts = toDate.Subtract(fromDate.AddDays(-1));
                        Double wholeMonth = Convert.ToDouble(((ts.Days) / 30).ToString().Split(".".ToCharArray())[0]);
                        ts =
                            new DateTime(fromDate.Year, fromDate.Month,
                                         DateTime.DaysInMonth(fromDate.Year, fromDate.Month)).Subtract(
                                fromDate.AddDays(-1));
                        returnValue = wholeMonth +
                                      Convert.ToDouble(ts.Days / DateTime.DaysInMonth(fromDate.Year, fromDate.Month));
                    }
                    else
                    {
                        //to add checking for both part first and last
                        //Only part of the last month is included
                        ts = toDate.Subtract(fromDate.AddDays(-1));
                        Double wholeMonth = Convert.ToDouble(((ts.Days) / 30).ToString().Split(".".ToCharArray())[0]);
                        ts = toDate.Subtract(new DateTime(toDate.Year, toDate.Month, 1).AddDays(-1));
                        returnValue = wholeMonth +
                                      Convert.ToDouble(ts.Days / DateTime.DaysInMonth(toDate.Year, toDate.Month));
                    }
                }
            }
            return returnValue;
        }


        /// <summary>
        /// Returns a string containing the time ago the date was compared to now.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>A string value</returns>
        public static string ToTimeAgo(this DateTime date)
        {
            TimeSpan timeSince = DateTime.Now.Subtract(date);

            if (timeSince.TotalMilliseconds < 1)
                return "not yet";

            if (timeSince.TotalMinutes < 1)
                return "just now";
            if (timeSince.TotalMinutes < 2)
                return "1 minute ago";
            if (timeSince.TotalMinutes < 60)
                return string.Format("{0} minutes ago", timeSince.Minutes);
            if (timeSince.TotalMinutes < 120)
                return "1 hour ago";
            if (timeSince.TotalHours < 24)
                return string.Format("{0} hours ago", timeSince.Hours);
            if (timeSince.TotalDays == 1)
                return "yesterday";
            if (timeSince.TotalDays < 7)
                return string.Format("{0} days ago", timeSince.Days);
            if (timeSince.TotalDays < 14)
                return "last week";
            if (timeSince.TotalDays < 21)
                return "2 weeks ago";
            if (timeSince.TotalDays < 28)
                return "3 weeks ago";
            if (timeSince.TotalDays < 60)
                return "last month";
            if (timeSince.TotalDays < 365)
                return string.Format("{0} months ago", Math.Round(timeSince.TotalDays / 30));
            if (timeSince.TotalDays < 730)
                return "last year";

            //last but not least...
            return string.Format("{0} years ago", Math.Round(timeSince.TotalDays / 365));
        }

        /// <summary>
        /// Calculate time duration
        /// </summary>
        /// <param name="date"></param>
        /// <param name="subtract">Subtract factor.</param>
        /// <returns></returns>
        public static TimeSpan TimeDuration(this DateTime date, DateTime subtract)
        {
            TimeSpan span;
            var dateSpan = new TimeSpan(date.Hour, date.Minute, date.Second);
            var subtractSpan = new TimeSpan(subtract.Hour, subtract.Minute, subtract.Second);

            span = dateSpan.Subtract(subtractSpan);

            if(span.Hours < 0 || span.Minutes < 0|| span.Seconds < 0)
                span = new TimeSpan(24,0,0).Subtract(new TimeSpan(Math.Abs(span.Hours),Math.Abs(span.Minutes),Math.Abs(span.Seconds)));

            return span;
        }

        /// <summary>
        /// Compare Time only, regardless of the date.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int TimeCompareTo(this DateTime first, DateTime second)
        {
            var firstSpan = new TimeSpan(first.Hour, first.Minute, first.Second);
            var seccondSpan = new TimeSpan(second.Hour, second.Minute, second.Second);

            return firstSpan.CompareTo(seccondSpan);
        }

        /// <summary>
        /// Returns time span period name.
        /// </summary>
        public static string GetPeriodName(this TimeSpan time)
        {
            if(time.Hours > 0)
            {
                return string.Format(StaticKeys.HoursPeriodName ,time.Hours > 1 ? "s" : string.Empty);
            }
           
            if (time.Minutes > 0)
            {
                return string.Format(StaticKeys.MinutesPeriodName, time.Minutes > 1 ? "s" : string.Empty);
            }

            if (time.Seconds > 0)
            {
                return string.Format(StaticKeys.SecondsPeriodName, time.Seconds > 1 ? "s" : string.Empty);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets next day of week.
        /// </summary>
        public static DayOfWeek NextDayOfWeek(this DateTime dateTime)
        {
            if ((int) dateTime.DayOfWeek == 7)
                return (DayOfWeek) 1;

            return dateTime.DayOfWeek + 1;
        }

        /// <summary>
        /// Gets next day of week.
        /// </summary>
        public static DayOfWeek NextDayOfWeek(this DayOfWeek dayOfWeek)
        {
            if ((int)dayOfWeek == 7)
                return (DayOfWeek)1;

            return dayOfWeek + 1;
        }

        /// <summary>
        /// Gets Age for current date of birth.
        /// </summary>
        public static int Age(this DateTime dateOfBirth)
        {
            var now = DateTime.Today;
            int age = now.Year - dateOfBirth.Year;
            if (now < dateOfBirth.AddYears(age)) age--;

            return age;
        }

        #endregion
    }
}

