using System.Collections.Generic;
using System.Linq;
using System;

namespace PriorityOrder
{
    /// <summary>
    /// Enumerable that contain order by priorities methods.<para/>
    /// StackOverflow answers:<para/>
    /// * <see href="https://stackoverflow.com/questions/12199668/in-c-what-is-the-best-way-to-sort-a-list-of-objects-by-a-string-property-and-g/78045482#78045482">Order by property name (string)</see><para/>
    /// * <see href="https://stackoverflow.com/questions/42550992/c-sharp-sort-list-by-enum/77618384#77618384">Order by property name (enum)</see><para/>
    /// 
    /// See also on <see href="https://github.com/kurnakovv/PriorityOrder/wiki">GitHub</see>
    /// </summary>
    public static class PriorityEnumerable
    {
        /// <summary>
        /// Order <paramref name="source"/> by <paramref name="priorities"/>.<para/>
        ///
        /// <example>
        /// <code>
        /// 
        /// <para/>// Simple code example
        /// <para/>var types = new string[] { "SUPER LOW", "LOW", "HIGH", "SUPER SUPER LOW", "MEDIUM" };
        /// <para/>var output = types.OrderByPriority(x => x, new string[] { "HIGH", "MEDIUM", "LOW" }).ToArray();
        /// <para/>// output: "HIGH", "MEDIUM", "LOW", "SUPER LOW", "SUPER SUPER LOW"
        /// 
        /// </code>
        /// </example>
        /// 
        /// Throw <see cref="ArgumentNullException"/> when <paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="priorities"/> is <see langword="null" />.<para/>
        /// Throw <see cref="ArgumentException"/> when <paramref name="priorities"/> contain duplicate values.
        /// </summary>
        /// 
        /// <remarks>
        /// StackOverflow answers:<para/>
        /// * <see href="https://stackoverflow.com/questions/12199668/in-c-what-is-the-best-way-to-sort-a-list-of-objects-by-a-string-property-and-g/78045482#78045482">Order by property name (string)</see><para/>
        /// * <see href="https://stackoverflow.com/questions/42550992/c-sharp-sort-list-by-enum/77618384#77618384">Order by property name (enum)</see><para/>
        /// 
        /// See also on <see href="https://github.com/kurnakovv/PriorityOrder/wiki/PriorityEnumerable.OrderByPriority(IEnumerable-priorities)">GitHub</see>
        /// </remarks>
        /// 
        /// <typeparam name="TSource"><paramref name="source"/> type.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="priorities">Elements that determine the order of enumerable.</param>
        /// 
        /// <returns>New ordered enumerable by <paramref name="priorities"/>.</returns>
        /// 
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IOrderedEnumerable<TSource> OrderByPriority<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEnumerable<TKey> priorities
        )
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            if (priorities == null)
            {
                throw new ArgumentNullException(nameof(priorities));
            }
            if (priorities.GroupBy(x => x).Any(x => x.Count() > 1))
            {
                throw new ArgumentException($"{nameof(priorities)} contain duplicate values.", nameof(priorities));
            }
            var priorityDict = new Dictionary<TKey, int>();
            for (int i = 0; i < priorities.Count(); i++)
            {
                priorityDict.Add(priorities.ElementAt(i), i);
            }
            return source.OrderBy(x =>
                priorityDict.TryGetValue(keySelector(x), out int value)
                    ? value
                    : priorityDict.Count + 1
            );
        }

        /// <summary>
        /// Order <paramref name="source"/> by <paramref name="priorities"/>.<para/>
        /// Uses <seealso cref="OrderByPriority{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IEnumerable{TKey})"/> internally.
        ///
        /// <example>
        /// <code>
        ///
        /// <para/>// Simple code example
        /// <para/>var types = new string[] { "SUPER LOW", "LOW", "HIGH", "SUPER SUPER LOW", "MEDIUM" };
        /// <para/>var output = types.OrderByPriority(x => x, "HIGH", "MEDIUM", "LOW").ToArray();
        /// <para/>// output: "HIGH", "MEDIUM", "LOW", "SUPER LOW", "SUPER SUPER LOW"
        ///
        /// </code>
        /// </example>
        ///
        /// Throw <see cref="ArgumentNullException"/> when <paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="priorities"/> is <see langword="null" />.<para/>
        /// Throw <see cref="ArgumentException"/> when <paramref name="priorities"/> contain duplicate values.
        /// </summary>
        ///
        /// <remarks>
        /// StackOverflow answers:<para/>
        /// * <see href="https://stackoverflow.com/questions/12199668/in-c-what-is-the-best-way-to-sort-a-list-of-objects-by-a-string-property-and-g/78045482#78045482">Order by property name (string)</see><para/>
        /// * <see href="https://stackoverflow.com/questions/42550992/c-sharp-sort-list-by-enum/77618384#77618384">Order by property name (enum)</see><para/>
        ///
        /// See also on <see href="https://github.com/kurnakovv/PriorityOrder/wiki/PriorityEnumerable.OrderByPriority(IEnumerable-priorities)">GitHub</see>
        /// </remarks>
        ///
        /// <typeparam name="TSource"><paramref name="source"/> type.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="priorities">Elements that determine the order of enumerable.</param>
        ///
        /// <returns>New ordered enumerable by <paramref name="priorities"/>.</returns>
        ///
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IOrderedEnumerable<TSource> OrderByPriority<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            params TKey[] priorities
        )
        {
            return source.OrderByPriority(keySelector, priorities.ToList());
        }
    }
}
