using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.Documents
{
    public static class IEnumerableExtensions
    {
        public static List<T> GetRow<T>(this List<List<T>> collection, int row)
        {
            return collection[row];
        }

        public static List<T> GetColumn<T>(this List<List<T>> collection, int col)
        {
            var list = new List<T>();

            for (int i = 0; i < collection.Count; i++)
            {
                var row = collection[i];

                if (row.Count > col)
                {
                    list.Add(row[col]);
                }
                else
                {
                    list.Add(default);
                }
            }

            return list;
        }

    }
}
