using RDK.Core.Mathematics;
using RDK.Core.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDK.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static string ByteArrayToString(this byte[] byteArray)
        {
            StringBuilder sBuilder = new (byteArray.Length);

            foreach (byte oneByte in byteArray)
            {
                sBuilder.Append(oneByte.ToString("X2"));
            }

            return sBuilder.ToString().ToLower();
        }

        public static string EncodeByteArray(this byte[] byteArray)
        {
            StringBuilder sBuilder = new(byteArray.Length);

            foreach (byte oneByte in byteArray)
            {
                sBuilder.Append((char)oneByte);
            }

            return sBuilder.ToString();
        }

        public static void Move<T>(this List<T> list, T obj, int newIndex) where T : ICloneable
        {
            list.Remove(obj);
            list.Insert(newIndex, obj);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new ();
            return source.Where(element => knownKeys.Add(keySelector(element)));
        }

        /// <summary>
        ///     Avoids if statements when building predicates and lambdas for a query.
        ///     Useful when you don't know at compile time whether a filter should apply.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate) 
            => (condition == true) ? source.Where(predicate) : source;

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
            => (condition == true) ? source.Where(predicate) : source;

        public static bool CompareEnumerable<T>(this IEnumerable<T> firstIE, IEnumerable<T> secondIE)
        {
            if (firstIE.GetType() != secondIE.GetType())
            {
                return false;
            }

            T[] firstArray = firstIE.ToArray();
            T[] secondArray = secondIE.ToArray();

            if (firstArray.Length != secondArray.Length)
            {
                return false;
            }

            return !firstArray.Except(secondArray).Any() && !secondArray.Except(firstArray).Any();
        }

        public static T MaxOf<T, T1>(this IList<T> collection, Func<T, T1> selector) where T1 : IComparable<T1>
        {
            if (collection.Count == 0) return default(T);

            T maxT = collection[0];
            T1 maxT1 = selector(maxT);

            for (int i = 1; i < collection.Count; i++)
            {
                T1 currentT1 = selector(collection[i]);

                if (currentT1.CompareTo(maxT1) <= 0) continue;

                maxT = collection[i];
                maxT1 = currentT1;
            }
            return maxT;
        }

        public static T MinOf<T, T1>(this IList<T> collection, Func<T, T1> selector) where T1 : IComparable<T1>
        {
            if (collection.Count == 0) return default(T);

            T maxT = collection[0];
            T1 maxT1 = selector(maxT);

            for (int i = 1; i < collection.Count; i++)
            {
                T1 currentT1 = selector(collection[i]);
                if (currentT1.CompareTo(maxT1) >= 0) continue;

                maxT = collection[i];
                maxT1 = currentT1;
            }
            return maxT;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            AsyncRandom rand = new();

            T[] elements = enumerable.ToArray();
            // Note i > 0 to avoid final pointless iteration
            for (int i = elements.Length - 1; i > 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                int swapIndex = rand.Next(i + 1);
                T tmp = elements[i];
                elements[i] = elements[swapIndex];
                elements[swapIndex] = tmp;
            }
            // Lazily yield (avoiding aliasing issues etc)
            return elements;
        }

        public static IEnumerable<T> ShuffleLinq<T>(this IEnumerable<T> enumerable)
        {
            CryptoRandom rand = new ();

            return enumerable.OrderBy(x => rand.NextDouble());
        }

        public static IEnumerable<T> ShuffleWithProbabilities<T>(this IEnumerable<T> enumerable, IEnumerable<int> probabilities)
        {
            Random rand = new ();

            List<T> elements = enumerable.ToList();
            T[] result = new T[elements.Count];
            List<int> indices = probabilities.ToList();

            if (elements.Count != indices.Count) throw new Exception("Probabilities must have the same length that the enumerable");

            int sum = indices.Sum();

            if (sum == 0)
            {
                return Shuffle(elements);
            }

            for (int i = 0; i < result.Length; i++)
            {
                int randInt = rand.Next(sum + 1);
                int currentSum = 0;
                for (int j = 0; j < indices.Count; j++)
                {
                    currentSum += indices[j];

                    if (currentSum >= randInt)
                    {
                        result[i] = elements[j];

                        sum -= indices[j];
                        indices.RemoveAt(j);
                        elements.RemoveAt(j);
                        break;
                    }
                }
            }

            return result;
        }

        public static T RandomElementOrDefault<T>(this IEnumerable<T> enumerable)
        {
            Random rand = new ();
            int count = enumerable.Count();

            if (count <= 0)
                return default;

            return enumerable.ElementAt(rand.Next(count));
        }

        public static string[] ToStringArr(this IEnumerable collection)
        {
            List<string> strs = new ();
            IEnumerator colEnum = collection.GetEnumerator();
            while (colEnum.MoveNext())
            {
                object current = colEnum.Current;
                if (current != null)
                {
                    strs.Add(current.ToString());
                }
            }
            return strs.ToArray();
        }

        public static void AddRange<T, S>(this Dictionary<T, S> source, Dictionary<T, S> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("Collection is null");
            }

            foreach (KeyValuePair<T, S> item in collection.Where(item => !source.ContainsKey(item.Key)))
            {
                source.Add(item.Key, item.Value);
            }
        }

        public static T[] Concat<T>(this T[] x, T[] y)
        {
            if (x == null)
            {
                throw new ArgumentNullException("x");
            }
                
            if (y == null)
            {
                throw new ArgumentNullException("y");
            }

            int oldLen = x.Length;
            Array.Resize(ref x, x.Length + y.Length);
            Array.Copy(y, 0, x, oldLen, y.Length);

            return x;
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) => dict.TryGetValue(key, out TValue val) ? val : default(TValue);

        public static T[] Add<T>(this T[] array, T item) => array.Concat(new T[] { item });

        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable => listToClone.Select(obj => (T)obj.Clone()).ToList();

        public static string ToStringCol(this ICollection collection, string conj) => collection != null ? string.Join(conj, ToStringArr(collection)) : "(null)";

        public static string ToString(this IEnumerable collection, string conj) => collection != null ? string.Join(conj, ToStringArr(collection)) : "(null)";

        public static T GetOrDefault<T>(this IList<T> list, int index) => index >= list.Count ? default : list[index];

        /// <summary>
        /// Converts an enumeration of groupings into a Dictionary of those groupings.
        /// </summary>
        /// <typeparam name="TKey">Key type of the grouping and dictionary.</typeparam>
        /// <typeparam name="TValue">Element type of the grouping and dictionary list.</typeparam>
        /// <param name="groupings">The enumeration of groupings from a GroupBy() clause.</param>
        /// <returns>A dictionary of groupings such that the key of the dictionary is TKey type and the value is List of TValue type.</returns>
        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groupings) 
            => groupings.ToDictionary(group => group.Key, group => group.ToList());
    }
}
