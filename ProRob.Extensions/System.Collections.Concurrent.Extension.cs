using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.Extensions.Collections.Concurrent
{
    public static class ConcurrentCollectionsExtensions
    {
        public static void Clear<T>(this BlockingCollection<T> bc)
        {
            //Eliminazione elementi collezione
            while (bc.TryTake(out _)) { };
        }

        public static void Clear<T>(this ConcurrentBag<T> bc)
        {
            //Eliminazione elementi collezione
            while (bc.TryTake(out _)) { };
        }
    }
}
