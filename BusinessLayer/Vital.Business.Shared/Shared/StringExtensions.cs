using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;

namespace Vital.Business.Shared.Shared
{
    public static class StringExtensions
    {
        #region Private Variables

        private static readonly Regex WebUrlExpression = new Regex(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex EmailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex StripHTMLExpression = new Regex("<\\S[^><]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        private static readonly char[] IllegalUrlCharacters = new[] { ';', '/', '\\', '?', ':', '@', '&', '=', '+', '$', ',', '<', '>', '#', '%', '.', '!', '*', '\'', '"', '(', ')', '[', ']', '{', '}', '|', '^', '`', '~', '–', '‘', '’', '“', '”', '»', '«' };

        #endregion

        #region Public Methods

        public static bool ToBoolean(this string src)
        {
            bool result;
            bool.TryParse(src, out result);
            return result;
        }

        public static int ToInteger(this string src)
        {
            int result;
            int.TryParse(src, out result);
            return result;
        }

        public static bool GuidTryParse(this string s, out Guid result)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            try
            {
                result = new Guid(s);
                return true;
            }
            catch (FormatException)
            {
                result = Guid.Empty;
                return false;
            }
            catch (OverflowException)
            {
                result = Guid.Empty;
                return false;
            }
        }

        public static string ComputeHash(this string value)
        {
            string hash = value;

            if (value != null)
            {
                var md5 = new MD5CryptoServiceProvider();
                byte[] data = Encoding.ASCII.GetBytes(value);
                data = md5.ComputeHash(data);
                hash = "";
                for (int i = 0; i < data.Length; i++)
                {
                    hash += data[i].ToString("x2");
                }
            }

            return hash;
        }

        /// <summary>
        /// Checks if the file extension is in an image format.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <returns></returns>
        public static bool IsInImageFormats(this string extension)
        {
            var imagesFormat = new List<string>
                                   {
                                       ".PNG",
                                       ".JPG",
                                       ".JPE",
                                       ".JPEG",
                                       ".GIF",
                                       ".TIFF",
                                       ".BMP"
                                   };

            return imagesFormat.Any(c => extension.ToUpper() == c);
        }

        /// <summary>
        /// Trims the size of the on.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxSize">Size of the max.</param>
        /// <returns></returns>
        public static string TrimOnSize(this string value, int maxSize)
        {
            if (value.Length <= maxSize)
                return value;
             
            return string.Concat(value.Substring(0, maxSize), "...");
        }

        [DebuggerStepThrough]
        public static bool IsWebUrl(this string target)
        {
            return !string.IsNullOrEmpty(target) && WebUrlExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static bool IsEmail(this string target)
        {
            return !string.IsNullOrEmpty(target) && EmailExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static string NullSafe(this string target)
        {
            return (target ?? string.Empty).Trim();
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string target, params object[] args)
        {
            Check.Argument.IsNotEmpty(target, "target");

            return string.Format(CultureInfo.CurrentCulture, target, args);
        }

        [DebuggerStepThrough]
        public static string Hash(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");

            using (MD5 md5 = MD5.Create())
            {
                byte[] data = Encoding.Unicode.GetBytes(target);
                byte[] hash = md5.ComputeHash(data);

                return Convert.ToBase64String(hash);
            }
        }

        [DebuggerStepThrough]
        public static string WrapAt(this string target, int index)
        {
            const int DotCount = 3;

            Check.Argument.IsNotEmpty(target, "target");
            Check.Argument.IsNotNegativeOrZero(index, "index");

            return (target.Length <= index) ? target : string.Concat(target.Substring(0, index - DotCount), new string('.', DotCount));
        }

        [DebuggerStepThrough]
        public static string StripHtml(this string target)
        {
            return StripHTMLExpression.Replace(target, string.Empty);
        }

        [DebuggerStepThrough]
        public static Guid ToGuid(this string target)
        {
            Guid result = Guid.Empty;

            if ((!string.IsNullOrEmpty(target)) && (target.Trim().Length == 22))
            {
                string encoded = string.Concat(target.Trim().Replace("-", "+").Replace("_", "/"), "==");

                try
                {
                    byte[] base64 = Convert.FromBase64String(encoded);

                    result = new Guid(base64);
                }
                catch (FormatException)
                {
                }
            }

            return result;
        }

        [DebuggerStepThrough]
        public static T ToEnum<T>(this string target, T defaultValue) where T : IComparable, IFormattable
        {
            T convertedValue = defaultValue;

            if (!string.IsNullOrEmpty(target))
            {
                try
                {
                    convertedValue = (T)Enum.Parse(typeof(T), target.Trim(), true);
                }
                catch (ArgumentException)
                {
                }
            }

            return convertedValue;
        }

        [DebuggerStepThrough]
        public static string ToLegalUrl(this string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return target;
            }

            target = target.Trim();

            if (target.IndexOfAny(IllegalUrlCharacters) > -1)
            {
                foreach (char character in IllegalUrlCharacters)
                {
                    target = target.Replace(character.ToString(CultureInfo.CurrentCulture), string.Empty);
                }
            }

            target = target.Replace(" ", "-");

            while (target.Contains("--"))
            {
                target = target.Replace("--", "-");
            }

            return target;
        }

        /// <summary>
        /// Replace spaces in string used for search to search through each word
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ToFullTextSearch(this string target)
        {
            return target.Replace(" ", "%").Replace("_","[_]");
        }

        #endregion
    }
}