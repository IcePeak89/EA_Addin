using System;

namespace FunctionalAllocator.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Convert string to given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ConvertToValue<T>(this string value)
        {
            if (typeof(T) == typeof(Guid))
                return (T)(dynamic)Guid.Parse(value);
            if (typeof(T).IsEnum && Enum.IsDefined(typeof(T), value))
                return (T)Enum.Parse(typeof(T), value);
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
