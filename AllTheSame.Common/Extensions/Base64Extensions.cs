using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AllTheSame.Common.Logging;

namespace AllTheSame.Common.Extensions
{
    /// <summary>
    ///     Base64Extensions
    /// </summary>
    public static class Base64Extensions
    {
        /// <summary>
        ///     Bitmaps to base64.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static string BitmapToBase64(this Image input)
        {
            try
            {
                var base64 = default(string);
                using (var memory = new MemoryStream())
                {
                    input.Save(memory, ImageFormat.Jpeg);

                    base64 = Convert.ToBase64String(memory.ToArray());
                    if (memory != null)
                        memory.Close();
                }

                return base64;
            }
            catch (ObjectDisposedException odex)
            {
                Audit.Log.Error("ObjectDisposedException :: Converting ToBase64String", odex);

                return null;
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Converting ToBase64String", e);

                return null;
            }
        }

        /// <summary>
        ///     Bitmaps from base64.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static Bitmap BitmapFromBase64(this string input)
        {
            try
            {
                var bitmap = default(Bitmap);
                using (var memory = new MemoryStream(Convert.FromBase64String(input)))
                {
                    bitmap = new Bitmap(memory);
                    if (memory != null)
                        memory.Close();
                }
                return bitmap;
            }
            catch (ObjectDisposedException odex)
            {
                Audit.Log.Error("ObjectDisposedException :: Converting FromBase64String", odex);

                return null;
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Converting FromBase64String", e);

                return null;
            }
        }
    }
}