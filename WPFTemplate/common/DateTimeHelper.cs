using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFTemplate.common
{
    internal class DateTimeHelper
    {
        private static System.Globalization.Calendar cal = new GregorianCalendar();

        public static DateTime? AddDays(DateTime time, int days)
        {
            try
            {
                return cal.AddDays(time, days);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public static DateTime? AddMonths(DateTime time, int months)
        {
            try
            {
                return cal.AddMonths(time, months);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public static DateTime? AddYears(DateTime time, int years)
        {
            try
            {
                return cal.AddYears(time, years);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public static DateTime? SetYear(DateTime date, int year)
        {
            return AddYears(date, year - date.Year);
        }

        public static DateTime? SetYearMonth(DateTime date, DateTime yearMonth)
        {
            DateTime? result = SetYear(date, yearMonth.Year);
            if (result.HasValue)
            {
                result = AddMonths(result.Value, yearMonth.Month - date.Month);
            }

            return result;
        }

        public static int CompareDays(DateTime dt1, DateTime dt2)
        {
            return DateTime.Compare(DiscardTime(dt1).Value, DiscardTime(dt2).Value);
        }

        public static int CompareYearMonth(DateTime dt1, DateTime dt2)
        {
            return (dt1.Year - dt2.Year) * 12 + (dt1.Month - dt2.Month);
        }

        public static int DecadeOfDate(DateTime date)
        {
            return date.Year - date.Year % 10;
        }

        public static DateTime DiscardDayTime(DateTime d)
        {
            return new DateTime(d.Year, d.Month, 1, 0, 0, 0);
        }

        public static DateTime? DiscardTime(DateTime? d)
        {
            if (!d.HasValue)
            {
                return null;
            }

            return d.Value.Date;
        }

        public static int EndOfDecade(DateTime date)
        {
            return DecadeOfDate(date) + 9;
        }

        public static DateTimeFormatInfo GetCurrentDateFormat()
        {
            return GetDateFormat(CultureInfo.CurrentCulture);
        }

        internal static CultureInfo GetCulture(FrameworkElement element)
        {
            return CultureInfo.CurrentCulture;
        }

        internal static DateTimeFormatInfo GetDateFormat(CultureInfo culture)
        {
            if (culture.Calendar is GregorianCalendar)
            {
                return culture.DateTimeFormat;
            }

            GregorianCalendar gregorianCalendar = null;
            DateTimeFormatInfo dateTimeFormatInfo = null;
            System.Globalization.Calendar[] optionalCalendars = culture.OptionalCalendars;
            foreach (System.Globalization.Calendar calendar in optionalCalendars)
            {
                if (calendar is GregorianCalendar)
                {
                    if (gregorianCalendar == null)
                    {
                        gregorianCalendar = (calendar as GregorianCalendar);
                    }

                    if (((GregorianCalendar)calendar).CalendarType == GregorianCalendarTypes.Localized)
                    {
                        gregorianCalendar = (calendar as GregorianCalendar);
                        break;
                    }
                }
            }

            if (gregorianCalendar == null)
            {
                dateTimeFormatInfo = ((CultureInfo)CultureInfo.InvariantCulture.Clone()).DateTimeFormat;
                dateTimeFormatInfo.Calendar = new GregorianCalendar();
            }
            else
            {
                dateTimeFormatInfo = ((CultureInfo)culture.Clone()).DateTimeFormat;
                dateTimeFormatInfo.Calendar = gregorianCalendar;
            }

            return dateTimeFormatInfo;
        }

        public static bool InRange(DateTime date, CalendarDateRange range)
        {
            return InRange(date, range.Start, range.End);
        }

        public static bool InRange(DateTime date, DateTime start, DateTime end)
        {
            if (CompareDays(date, start) > -1 && CompareDays(date, end) < 1)
            {
                return true;
            }

            return false;
        }

        public static string ToDayString(DateTime? date, CultureInfo culture)
        {
            string result = string.Empty;
            DateTimeFormatInfo dateFormat = GetDateFormat(culture);
            if (date.HasValue && dateFormat != null)
            {
                result = date.Value.Day.ToString(dateFormat);
            }

            return result;
        }

        public static string ToDecadeRangeString(int decade, FrameworkElement fe)
        {
            string result = string.Empty;
            DateTimeFormatInfo dateFormat = GetDateFormat(GetCulture(fe));
            if (dateFormat != null)
            {
                bool flag = fe.FlowDirection == FlowDirection.RightToLeft;
                result = string.Concat(str2: (flag ? decade : (decade + 9)).ToString(dateFormat), str0: (flag ? (decade + 9) : decade).ToString(dateFormat), str1: "-");
            }

            return result;
        }

        public static string ToYearMonthPatternString(DateTime? date, CultureInfo culture)
        {
            string result = string.Empty;
            DateTimeFormatInfo dateFormat = GetDateFormat(culture);
            if (date.HasValue && dateFormat != null)
            {
                result = date.Value.ToString(dateFormat.YearMonthPattern, dateFormat);
            }

            return result;
        }

        public static string ToYearString(DateTime? date, CultureInfo culture)
        {
            string result = string.Empty;
            DateTimeFormatInfo dateFormat = GetDateFormat(culture);
            if (date.HasValue && dateFormat != null)
            {
                result = date.Value.Year.ToString(dateFormat);
            }

            return result;
        }

        public static string ToAbbreviatedMonthString(DateTime? date, CultureInfo culture)
        {
            string result = string.Empty;
            DateTimeFormatInfo dateFormat = GetDateFormat(culture);
            if (date.HasValue && dateFormat != null)
            {
                string[] abbreviatedMonthNames = dateFormat.AbbreviatedMonthNames;
                if (abbreviatedMonthNames != null && abbreviatedMonthNames.Length != 0)
                {
                    result = abbreviatedMonthNames[(date.Value.Month - 1) % abbreviatedMonthNames.Length];
                }
            }

            return result;
        }

        public static string ToLongDateString(DateTime? date, CultureInfo culture)
        {
            string result = string.Empty;
            DateTimeFormatInfo dateFormat = GetDateFormat(culture);
            if (date.HasValue && dateFormat != null)
            {
                result = date.Value.Date.ToString(dateFormat.LongDatePattern, dateFormat);
            }

            return result;
        }
    }
}
