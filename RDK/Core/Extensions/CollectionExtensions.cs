using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDK.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static string ByteArrayToString(this byte[] bytes)
        {
            StringBuilder sBuilder = new (bytes.Length);

            foreach (byte oneByte in bytes)
            {
                sBuilder.Append(oneByte.ToString("X2"));
            }

            return sBuilder.ToString().ToLower();
        }

        public static string EncodeByteArray(this byte[] bytes)
        {
            StringBuilder sBuilder = new(bytes.Length);

            foreach (byte oneByte in bytes)
            {
                sBuilder.Append((char)oneByte);
            }

            return sBuilder.ToString();
        }

        public static void Move<T>(this List<T> list, T type, int newIndex) where T : ICloneable
        {
            list.Remove(type);
            list.Insert(newIndex, type);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            return source.Where(element => knownKeys.Add(keySelector(element)));
        }

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

        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable => listToClone.Select(type => (T)type.Clone()).ToList();


    }
}
