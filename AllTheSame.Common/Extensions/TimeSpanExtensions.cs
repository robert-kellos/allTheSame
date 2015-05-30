using System;

namespace AllTheSame.Common.Extensions
{
    /// <summary>
    ///     TimeSpanExtensions
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        ///     Gets the description.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string GetDescription(this TimeSpan input)
        {
            var diff = input.Ticks > 0 ? "vor" + " " : "in" + " ";
            var min = input.Minutes;
            var std = input.Hours;
            var tage = input.Days;

            if (tage == 0)
            {
                if (std > 1) diff += std + " " + "Stunden" + " ";
                else if (std == 1) diff += std + " " + "Stunde" + " ";
                if (min == 0) diff += min + " " + "Minute";
                else diff += min + " " + "Minuten";
            }
            else
            {
                if (tage == 1) diff += tage + " " + "Tag";
                else diff += tage + " " + "Tagen";
            }

            return diff;
        }

        /// <summary>
        ///     Agoes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static DateTime Ago(this TimeSpan input)
        {
            return (DateTime.Now.Subtract(input));
        }
    }
}