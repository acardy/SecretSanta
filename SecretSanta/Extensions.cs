using System.Collections.Generic;
using System.Linq;

namespace SecretSanta
{
    public static class Extensions
    {
        public static TSource MextFrom<TSource>(this List<TSource> list, TSource item)
        {
            return list.ElementAt(list.IndexOf(item) + 1);
        }
    }
}
