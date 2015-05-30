using System;
using System.Globalization;

namespace AllTheSame.Common.Extensions
{
    /// <summary>
    ///     DateTimeExtensions
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     Gets the date time string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string GetDateTimeString(this DateTime input)
        {
            try
            {
                var format = new CultureInfo("en-US", true);
                return input.ToString("G", format);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        ///     Determines whether the specified start date is between.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="compareTime">if set to <c>true</c> [compare time].</param>
        /// <returns></returns>
        public static bool IsBetween(this DateTime input, DateTime startDate, DateTime endDate,
            bool compareTime = false)
        {
            return compareTime
                ? input >= startDate && input <= endDate
                : input.Date >= startDate.Date && input.Date <= endDate.Date;
        }

        /// <summary>
        ///     To the friendly date string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string ToFriendlyDateString(this DateTime input)
        {
            string formattedDate;

            if (input.Date == DateTime.Today)
            {
                formattedDate = "Today";
            }
            else if (input.Date == DateTime.Today.AddDays(-1))
            {
                formattedDate = "Yesterday";
            }
            else if (input.Date > DateTime.Today.AddDays(-6))
            {
                // *** Show the Day of the week
                formattedDate = input.ToString("dddd");
            }
            else
            {
                formattedDate = input.ToString("MMMM dd, yyyy");
            }

            //append the time portion to the output
            formattedDate += " @ " + input.ToString("t").ToLower();

            return formattedDate;
        }

        /// <summary>
        ///     Gets the time span till now.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static TimeSpan GetTimeSpanTillNow(this DateTime input)
        {
            return new TimeSpan(DateTime.Now.Ticks - input.Ticks);
        }

        /// <summary>
        ///     Gets the time span till now.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static TimeSpan GetTimeSpanTillNow(this DateTime? input)
        {
            return new TimeSpan(DateTime.Now.Ticks - Convert.ToDateTime(input).Ticks);
        }
    }
}