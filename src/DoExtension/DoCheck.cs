namespace DoExtension
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

#if NETSTANDARD2_1 || NET5_0
    using System.Diagnostics.CodeAnalysis;
#endif

    public static class DoCheck
    {
        public static bool IsNumeric(string? str)
        {
            if (IsEmpty(str))
            {
                return false;
            }
            return ObjectIsNumeric(str.FullTrim());
        }

        public static bool IsEmpty(
#if NETSTANDARD2_1 || NET5_0
            [NotNullWhen(false)]
            string? str
#else
            string str
#endif
       )
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                return true;
            }
            return false;
        }

        public static bool IsGuid(string str)
        {
            if (IsNotEmpty(str))
            {
                return Guid.TryParse(str.FullTrim(), out var _);
            }
            return false;
        }

        public static bool IsNotEmpty(
#if NETSTANDARD2_1 || NET5_0
            [NotNullWhen(true)]
            string? str
#else
            string str
#endif
        )
        {
            return !IsEmpty(str);
        }

        public static bool IsEmpty<T>(
#if NETSTANDARD2_1 || NET5_0
            [NotNullWhen(false)]
            IEnumerable<T>? list
#else
            IEnumerable<T> list
#endif

        )
        {
            if (list is null)
            {
                return true;
            }
            if (!list.Any())
            {
                return true;
            }
            return false;
        }

        public static bool IsNotEmpty<T>(
#if NETSTANDARD2_1 || NET5_0
            [NotNullWhen(false)]
            IEnumerable<T>? list
#else
            IEnumerable<T> list
#endif
       )
        {
            return !IsEmpty(list);
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
