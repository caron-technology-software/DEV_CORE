using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.Extensions.Collections
{
    public static class CollectionsGenericExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action.Invoke(item);
            }
        }

        public static IEnumerable<int> AllIndexesOf(this string str, string searchstring)
        {
            int minIndex = str.IndexOf(searchstring);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
            }
        }

        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> collection, int n)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n", "n must be 0 or greater");
            }

            LinkedList<T> temp = new LinkedList<T>();

            foreach (var value in collection)
            {
                temp.AddLast(value);
                if (temp.Count > n)
                {
                    temp.RemoveFirst();
                }
            }

            return temp;
        }

        public static void Print<T>(this List<List<T>> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                var elements = collection[i];

                for (int j = 0; j < elements.Count; j++)
                {
                    Console.WriteLine($"({i},{j}) {elements[j]}");
                }
            }
        }

        public static void Print<T>(this List<T> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                Console.WriteLine($"({i}) {collection[i]}");
            }
        }

        public static List<T> TakeLast<T>(this List<T> collection, int n)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n", "n must be 0 or greater");
            }

            if (n > collection.Count)
            {
                throw new ArgumentOutOfRangeException("n", "n must be lower or equal the collection count");
            }

            var temp = new List<T>(n);

            for (int i = n; n < collection.Count; i++)
            {
                temp.Add(collection[i]);
            }

            return temp;
        }
    }
}
