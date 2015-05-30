using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using AllTheSame.Common.Logging;

namespace AllTheSame.Common.Extensions
{
    /// <summary>
    ///     StringExtensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     The domain regex
        /// </summary>
        private static readonly Regex DomainRegex =
            new Regex(
                @"(((?<scheme>http(s)?):\/\/)?([\w-]+?\.\w+)+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\-\=\+\\\/\?\.\:\;\,]*)?)",
                RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     Determines whether this instance is numeric.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static bool IsNumeric(this string input)
        {
            var result = default(bool);

            try
            {
                long value;
                result = long.TryParse(input, NumberStyles.Integer,
                    NumberFormatInfo.InvariantInfo, out value);
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Checking if string is numeric", e);
            }

            return result;
        }

        /// <summary>
        ///     Determines whether [is valid email address].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(this string input)
        {
            var result = default(bool);

            try
            {
                var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                result = regex.IsMatch(input);
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Checking if string is a numeric", e);
            }

            return result;
        }

        /// <summary>
        ///     Determines whether [is valid phone number].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static bool IsValidPhoneNumber(this string input)
        {
            var result = default(bool);

            try
            {
                var regex = new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( x\d{0,})?");
                result = regex.IsMatch(input);
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Checking if string is a phone number", e);
            }

            return result;
        }

        /// <summary>
        ///     To the proper case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string ToProperCase(this string input)
        {
            var result = input;

            try
            {
                var cultureInfo = Thread.CurrentThread.CurrentCulture;
                var textInfo = cultureInfo.TextInfo;

                result = textInfo.ToTitleCase(input);
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Setting string to use proper casing", e);
            }

            return result;
        }

        /// <summary>
        ///     Returns string in camelCase format
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string s)
        {
            var prepString = s;
            //Build the titlecase string
            if (s.Contains(' '))
            {
                prepString = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant());
            }
            //Ensures that there is at-least two characters (so that the Substring method doesn't freak out)
            return (prepString.Length > 1) ? char.ToLowerInvariant(prepString[0]) + prepString.Substring(1) : prepString;
        }

        /// <summary>
        ///     To the link.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static string ToLink(this string input, string target = "_self")
        {
            var result = input;

            try
            {
                result = DomainRegex.Replace(
                    input,
                    match =>
                    {
                        var link = match.ToString();
                        var scheme = match.Groups["scheme"].Value == "https" ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

                        var url = new UriBuilder(link) {Scheme = scheme}.Uri.ToString();

                        return string.Format(@"<a href=""{0}"" target=""{1}"">{2}</a>", url, target, link);
                    });
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Converting string to hyper-link", e);
            }

            return result;
        }

        /// <summary>
        ///     Gets the date time string iso.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string GetDateTimeStringIso(this DateTime date)
        {
            var result = default(string);

            try
            {
                result = date.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Get ISO datetime string", e);
            }

            return result;
        }

        /// <summary>
        ///     To the date time.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string input)
        {
            var result = default(DateTime?);

            try
            {
                DateTime dtr;
                var tryDtr = DateTime.TryParse(input, out dtr);
                result = (tryDtr) ? dtr : new DateTime?();
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Get ISO datetime string", e);
            }

            return result;
        }

        /// <summary>
        ///     Bitmaps from base64.
        /// </summary>
        /// <param name="base64">The base64.</param>
        /// <returns></returns>
        public static Bitmap BitmapFromBase64(string base64)
        {
            try
            {
                Bitmap bitmap;
                using (var memory = new MemoryStream(Convert.FromBase64String(base64)))
                {
                    bitmap = new Bitmap(memory);
                }

                return bitmap;
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: pushing base64 string to memorystream bitmap", e);

                return null;
            }
        }

        /// <summary>
        ///     Gets the last.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="tailLength">Length of the tail.</param>
        /// <returns></returns>
        public static string GetLast(this string input, int tailLength)
        {
            var result = default(string);

            try
            {
                if (tailLength >= input.Length)
                    result = input;

                result = input.Substring(input.Length - tailLength);
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Get last part of string", e);
            }

            return result;
        }

        /// <summary>
        ///     Removes the last.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="removeCharacters">The remove characters.</param>
        /// <returns></returns>
        public static string RemoveLast(this string input, int removeCharacters)
        {
            var result = default(string);

            try
            {
                if (input.Length >= removeCharacters)
                    result = input.Substring(0, input.Length - removeCharacters);
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Remove last part of string", e);
            }

            return result;
        }

        /// <summary>
        ///     Removes the first.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="removeCharacters">The remove characters.</param>
        /// <returns></returns>
        public static string RemoveFirst(this string input, int removeCharacters)
        {
            var result = default(string);

            try
            {
                if (input.Length >= removeCharacters)
                    result = input.Substring(removeCharacters);
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Remove first part of string", e);
            }

            return result;
        }

        /// <summary>
        ///     Pads the begin characters.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="totalLength">The total length.</param>
        /// <param name="padChar">The pad character.</param>
        /// <returns></returns>
        public static string PadBeginCharacters(this string input, int totalLength, char padChar)
        {
            var result = default(string);

            try
            {
                if (input == null) result = string.Empty;
                while (input != null && input.Length < totalLength)
                    result = padChar + input;
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Pad first part of string with set characters", e);
            }

            return result;
        }

        /// <summary>
        ///     Pads the end characters.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="totalLength">The total length.</param>
        /// <param name="padChar">The pad character.</param>
        /// <returns></returns>
        public static string PadEndCharacters(this string input, int totalLength, char padChar)
        {
            var result = default(string);

            try
            {
                if (input == null) result = string.Empty;
                while (input != null && input.Length < totalLength)
                    result = input + padChar;
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Pad last part of string with set characters", e);
            }

            return result;
        }

        /// <summary>
        ///     Removes the text between.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="startDelimiter">The start delimiter.</param>
        /// <param name="endDelimiter">The end delimiter.</param>
        /// <returns></returns>
        public static string RemoveTextBetween(this string input, string startDelimiter, string endDelimiter)
        {
            var result = default(string);

            try
            {
                var startIndex = input.IndexOf(startDelimiter, StringComparison.CurrentCulture);
                var text = input.Substring(0, startIndex);
                var endIndex = input.IndexOf(endDelimiter, startIndex, StringComparison.CurrentCulture);

                result = text + input.Substring(endIndex + endDelimiter.Length);
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Removing text between delimeters", e);
            }

            return result;
        }

        /// <summary>
        ///     Shortens the string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns></returns>
        public static string ShortenString(this string input, int maxLength)
        {
            var result = default(string);

            try
            {
                if (string.IsNullOrEmpty(input)) return input;
                if (maxLength <= 3 | input.Length <= maxLength) return input;

                var factor = Math.Floor((input.Length)*0.75);
                var trimmed = input.Length - maxLength + 3;

                var firstPart = input.Substring(0, (int) (factor - (trimmed*0.75)));
                var lastPart = input.Substring((int) (factor + (trimmed*0.25)));

                result = (firstPart + "..." + lastPart);
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Removing text between delimeters", e);
                if (input != null) result = input.Substring(0, maxLength - 3) + "...";
            }

            return result;
        }

        /// <summary>
        ///     Removes the last seperator.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string RemoveLastSeperator(this string input)
        {
            var result = default(string);

            if (input == null) return null;
            if (string.IsNullOrEmpty(input)) return string.Empty;

            try
            {
                result = input.TrimEnd(' ');

                while (result.EndsWith(",") |
                       result.EndsWith(";") |
                       result.EndsWith("|") |
                       result.EndsWith("@") |
                       result.EndsWith("#") |
                       result.EndsWith("+") |
                       result.EndsWith("*") |
                       result.EndsWith("-") |
                       result.EndsWith("_"))
                {
                    result = result.Substring(0, result.Length - 1);
                }

                if (result.EndsWith(Environment.NewLine))
                    result = result.Substring(0, result.LastIndexOf(Environment.NewLine, StringComparison.Ordinal));
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Removing last separater from string", e);
            }

            return result;
        }

        /// <summary>
        ///     Gets the MD5 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string GetMd5Hash(this string input)
        {
            try
            {
                if (input == null) return string.Empty;

                var md5 = MD5.Create();
                var inputBytes = Encoding.ASCII.GetBytes(input);
                var hash = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();

                foreach (var bt in hash)
                {
                    sb.Append(bt.ToString("X2"));
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Getting Md5 Hash", e);

                return string.Empty;
            }
        }
    }
}