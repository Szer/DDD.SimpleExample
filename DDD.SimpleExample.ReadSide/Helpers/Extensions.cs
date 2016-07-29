using System;
using System.Data.Entity;

namespace DDD.SimpleExample.ReadSide.Helpers
{
    internal static class Extensions
    {
        public static T FindAggregate<T>(this DbSet<T> set, Guid id) where T : class
        {
            var key = string.Concat(typeof(T).Name, "-", id);
            return set.Find(key);
        }
    }
}