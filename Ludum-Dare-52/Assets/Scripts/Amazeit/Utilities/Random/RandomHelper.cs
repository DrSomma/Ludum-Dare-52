using System.Collections.Generic;
using System.Linq;

namespace Amazeit.Utilities.Random
{
    public static class RandomHelper
    {
        /// <summary>
        ///     <para>Return a random amound of objects from the array within count = [minInclusive..maxExclusive).</para>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="countMin"></param>
        /// <param name="countMax"></param>
        public static T[] Random<T>(this T[] array, int countMin, int countMax)
        {
            int count = UnityEngine.Random.Range(minInclusive: countMin, maxExclusive: countMax + 1);
            T[] list = new T[count];
            for (int i = 0; i < count; i++)
            {
                list[i] = array[UnityEngine.Random.Range(minInclusive: 0, maxExclusive: array.Length)];
            }

            return list;
        }

        public static T Random<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(minInclusive: 0, maxExclusive: array.Length)];
        }
        
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            IEnumerable<T> ts = enumerable as T[] ?? enumerable.ToArray();
            return ts.ElementAt(UnityEngine.Random.Range(minInclusive: 0, maxExclusive: ts.Count()));
        }
    }
}