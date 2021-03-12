namespace DoExtension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtension
    {
        public static ICollection<T> FluentAdd<T>(this ICollection<T> list, T obj)
        {
            list.Add(obj);
            return list;
        }

        public static ICollection<T> FluentAddRange<T>(this ICollection<T> list, IEnumerable<T> objs)
        {
            foreach (var obj in objs)
            {
                list.Add(obj);
            }
            return list;
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> mapFunction)
        {
            foreach (var item in list)
            {
                mapFunction(item);
            }
        }

        public static U GetValue<T, U>(this IDictionary<T, U> dict, T key)
        {
            if (!dict.TryGetValue(key, out var value))
            {
                throw new KeyNotFoundException($"Cannot find key {key}");
            }
            return value;
        }

        public static U GetValueOrDefault<T, U>(this IDictionary<T, U> dict, T key, U defaultValue = default)
        {
            if (!dict.TryGetValue(key, out var value))
            {
                return defaultValue;
            }
            return value;
        }

        public static bool IsIn<T>(this T source, params T[] list)
        {
            if (null == source)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (DoCheck.IsEmpty(list))
            {
                throw new ArgumentNullException(nameof(list));
            }
            return list.Contains(source);
        }

        public static bool IsIn<T>(this T source, IEnumerable<T> list)
        {
            if (null == source)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (DoCheck.IsEmpty(list))
            {
                throw new ArgumentNullException(nameof(list));
            }
            return list.Contains(source);
        }

        public static string Join<T>(this IEnumerable<T> list, string separator = ", ")
        {
            return string.Join(separator, list);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            return list.OrderBy(x => Guid.NewGuid());
        }
    }
}
