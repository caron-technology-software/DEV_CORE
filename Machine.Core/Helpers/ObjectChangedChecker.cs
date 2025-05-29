using System;

using ProRob.Extensions.Hashing;
using ProRob.Extensions.Object;

namespace Machine.Utility
{
    public class ObjectChangedChecker<T>
    {
        public string LastHash { get; private set; } = String.Empty;
        public bool IsChanged { get; private set; } = true;

        public T LastObject { get; private set; }

        public ObjectChangedChecker()
        {

        }

        public bool Check(T obj)
        {
            var currentHash = obj.GetSHA1Hash();

            if (currentHash != LastHash)
            {
                LastHash = currentHash;
                //GPI21 aggiunto a obj la funzione Clone() per essere sicuri di clonare l'instanza dell'oggetto:
                LastObject = obj.Clone();
                IsChanged = true;
            }
            else
            {
                IsChanged = false;
            }

            return IsChanged;
        }
    }
}