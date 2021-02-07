namespace DoExtension
{
    using System;
    using System.Collections.Generic;
#if NETSTANDARD2_1 || NET5_0
    using System.Diagnostics.CodeAnalysis;
#endif
    using System.Globalization;
    using System.Text.RegularExpressions;

    public static class StringExtension
    {
        private static readonly IEnumerable<char> trimList = new List<char>() { zw1, zw2, zw3, ' ', '\n', '\t', '\r' };
        private static readonly char zw1 = (char)int.Parse("200C", NumberStyles.AllowHexSpecifier);
        private static readonly char zw2 = (char)int.Parse("200b", NumberStyles.AllowHexSpecifier);
        private static readonly char zw3 = (char)int.Parse("FEFF", NumberStyles.AllowHexSpecifier);

        public static T EnumParse<T>(this string value)
        {
            return EnumParse<T>(value, false);
        }

        public static T EnumParse<T>(this string value, bool ignorecase)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value = value.Trim();

            if (value.Length == 0)
            {
                throw new ArgumentException("Must specify valid information for parsing in the string.", nameof(value));
            }

            var t = typeof(T);

            if (!t.IsEnum)
            {
                throw new ArgumentException("Type provided must be an Enum.", "T");
            }

            return (T)Enum.Parse(t, value, ignorecase);
        }

        public static string FullTrim(this string str, params char[] trimChars)
        {
            if (str.IsEmpty())
            {
                return string.Empty;
            }
            var list = new List<char>();
            list.AddRange(trimList);
            if (trimChars.Length > 0)
            {
                foreach (var item in trimChars)
                {
                    list.Add(item);
                }
            }
            return str.Trim(list.ToArray());
        }

        public static bool IsEmpty(
#if NETSTANDARD2_1 || NET5_0
            [NotNullWhen(false)] 
#endif
            this string str
        )
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                return true;
            }
            return false;
        }

        public static bool IsGuid(this string str)
        {
            if (str.IsNotEmpty())
            {
                return Guid.TryParse(str.FullTrim(), out var _);
            }
            return false;
        }

        public static bool IsNotEmpty(
#if NETSTANDARD2_1 || NET5_0
            [NotNullWhen(true)] 
#endif
            this string? str
        )
        {
            return str.IsEmpty() == false;
        }

        public static bool IsNumeric(this string? str)
        {
            if (str.IsEmpty())
            {
                return false;
            }
            return ObjectIsNumeric(str.FullTrim());
        }

        public static string Left(this string str, int len)
        {
            return str.Substring(0, len);
        }

        public static bool Match(this string value, string pattern)
        {
            var regex = new Regex(pattern);
            return regex.IsMatch(value);
        }

        public static string Remove(this string str, string strToRemove)
        {
            if (str.IsEmpty())
            {
                return "";
            }

            return str.Replace(strToRemove, "");
        }

        public static string Right(this string str, int len)
        {
#if NETSTANDARD2_1 || NET5_0
            return str[^len..];
#else
            return str.Substring(str.Length - len);
#endif
        }

        public static string SafeSubstring(this string? input, int start, int length)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (input.Length >= (start + length))
            {
                return input.Substring(start, length);
            }
#if NETSTANDARD2_1 || NET5_0
            return input.Length > start ? input[start..] : string.Empty;
#else
            return input.Length > start ? input.Substring(start) : string.Empty;
#endif
        }

        public static IEnumerable<string> Split(this string str, params char[] separator)
        {
            return str.Split(separator);
        }

        public static IEnumerable<string> SplitNoneEmpty(this string str, params char[] separator)
        {
            return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> SplitNoneEmpty(this string str, params string[] separator)
        {
            return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string StripHtml(this string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }

            return Regex.Replace(html, @"<[^>]*>", string.Empty);
        }

        public static bool ToBoolean(this string str)
        {
            if (bool.TryParse(str, out var value))
            {
                return value;
            }
            throw new InvalidCastException();
        }

        public static bool? ToBooleanOrDefault(this string str, bool? defualtValue = null)
        {
            if (bool.TryParse(str, out var value))
            {
                return value;
            }
            return defualtValue;
        }

        public static double ToDouble(this string str)
        {
            if (str.IsEmpty())
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (!str.IsNumeric())
            {
                throw new InvalidCastException();
            }
            return double.Parse(str.Trim());
        }

        public static double? ToDoubleOrDefault(this string str, double? defualtValue = null)
        {
            if (str.IsEmpty())
            {
                return defualtValue;
            }
            if (!str.IsNumeric())
            {
                return defualtValue;
            }
            return double.Parse(str.Trim());
        }

        public static Guid ToGuid(this string str)
        {
            if (!str.IsGuid())
            {
                throw new InvalidCastException();
            }
            return Guid.Parse(str);
        }

        public static Guid? ToGuidOrDefualt(this string str, Guid? defualtValue = null)
        {
            if (!str.IsGuid())
            {
                return defualtValue;
            }
            return Guid.Parse(str);
        }

        public static int ToInt(this string str)
        {
            if (str.IsEmpty())
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (!str.IsNumeric())
            {
                throw new InvalidCastException();
            }
            return int.Parse(str.Trim());
        }

        public static int? ToIntOrDefault(this string str, int? defualtValue = null)
        {
            if (str.IsEmpty())
            {
                return defualtValue;
            }
            if (!str.IsNumeric())
            {
                return defualtValue;
            }
            return int.Parse(str.Trim());
        }

        public static long ToLong(this string str)
        {
            if (str.IsEmpty())
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (!str.IsNumeric())
            {
                throw new InvalidCastException(nameof(str));
            }
            return long.Parse(str.Trim());
        }

        public static long? ToLongOrDefault(this string str, long? defualtValue = null)
        {
            if (str.IsEmpty())
            {
                return defualtValue;
            }
            if (!str.IsNumeric())
            {
                return defualtValue;
            }
            return long.Parse(str.Trim());
        }

        private static bool ObjectIsNumeric(object obj)
        {
            var isNum = double.TryParse(Convert.ToString(obj),
                                        NumberStyles.Any,
                                        NumberFormatInfo.InvariantInfo,
                                        out var _);
            return isNum;
        }
    }
}
