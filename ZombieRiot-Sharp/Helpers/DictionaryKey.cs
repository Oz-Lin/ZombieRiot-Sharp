using System.Collections.Generic;

namespace ZombieRiot_Sharp.Helpers
{
    public static class DictionaryKey
    {
        public static TKey KeyByValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TValue val) where TKey : notnull
        {
            TKey key = default!;
            foreach (KeyValuePair<TKey, TValue> pair in dict)
            {
                if (EqualityComparer<TValue>.Default.Equals(pair.Value, val))
                {
                    key = pair.Key;
                    break;
                }
            }
            return key;
        }
    }
}
