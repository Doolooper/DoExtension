namespace DoExtension
{
    using System;
    using System.Collections.Generic;
#if NETSTANDARD2_1 || NET5_0
    using System.Diagnostics.CodeAnalysis;
#endif
    using System.Linq;

    public static class EnumerableExtension
    {
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

        public static bool IsEmpty<T>(
#if NETSTANDARD2_1 || NET5_0
            [NotNullWhen(false)] 
#endif
            this IEnumerable<T> list

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

        public static bool IsIn<T>(this T source, params T[] list)
        {
            if (null == source)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return list.Contains(source);
        }

        public static bool IsIn<T>(this T source, IEnumerable<T> list)
        {
            if (null == source)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return list.Contains(source);
        }

        public static bool IsNotEmpty<T>(
#if NETSTANDARD2_1 || NET5_0
            [NotNullWhen(true)] 
#endif
            this IEnumerable<T> list
        )
        {
            return list.IsEmpty() == false;
        }

        public static string Join<T>(this IEnumerable<T> list, string separator = ", ")
        {
            return string.Join(separator, list);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            return list.OrderBy(x => Guid.NewGuid());
        }

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
    }
}
