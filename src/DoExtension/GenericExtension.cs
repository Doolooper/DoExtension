namespace DoExtension
{
    using System;
    using System.Collections.Generic;

    public static class GenericExtension
    {
        public static bool Between<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) > 0 && actual.CompareTo(upper) < 0;
        }

        public static bool BetweenAndEqual<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) <= 0;
        }

        public static IEnumerable<Enum> ToEnumerable(this Enum enumValue)
        {
            foreach (Enum value in Enum.GetValues(enumValue.GetType()))
            {
                if (enumValue.HasFlag(value) && Convert.ToInt64(value) != 0)
                {
                    yield return value;
                }
            }
        }

        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }
    }
}
