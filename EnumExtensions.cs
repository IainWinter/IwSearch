using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwSearch {
    /// <summary>
    ///     Provided useful extension methods to be used with an IEnumerable<T>
    /// </summary>
    public static class EnumExtensions {
        /// <summary>
        ///     Find the first index of a specific value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="value">Value to find</param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> data, T value) {
            return data.IndexOf(value, null);
        }

        /// <summary>
        ///     Find the first index of a specific value using a custom comparer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="value">Value to find</param>
        /// <param name="comparer">Custom comparer to be used instade of default</param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> data, T value, IEqualityComparer<T> comparer) {
            comparer = comparer ?? EqualityComparer<T>.Default;
            var found = data
                .Select((a, i) => new { a, i })
                .FirstOrDefault(x => comparer.Equals(x.a, value));

            return found == null ? -1 : found.i;
        }
    }
}
