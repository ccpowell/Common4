using System;
using System.Collections.Generic;
//using System.Web.Routing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DRCOG.Common.Collections
{
    public static class CollectionExtensions
    {
        public static IList<T> Insert<T>(this IList<T> list, T item, Int32 index)
        {
            list.Insert(index, item);
            return list;
        }

        public static List<T> Insert<T>(this List<T> list, T item, Int32 index)
        {
            list.Insert(index, item);
            return list;
        }

        public static IDictionary<TKey, TValue> Add<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value, TKey key)
        {
            dictionary.Add(key, value);
            return dictionary;
        }

        public static Dictionary<TKey, TValue> Add<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue value, TKey key)
        {
            dictionary.Add(key, value);
            return dictionary;
        }

        /// <summary>
        /// Create a dictionary
        /// ex. var dic = new { Name = "Frank", Age = 5 }.ToPropertyHash(); 
        /// </summary>
        /// <param name="item">object</param>
        /// <returns>Dictionary<string, object></returns>
        /// <see cref="http://www.richardbushnell.net/2007/12/12/aspnet-mvc-framework-made-public/"/>
        public static Dictionary<string, object> ToPropertyHash(this object item)
        {
            var props = from property in item.GetType().GetProperties()
                        select new { Name = property.Name, Value = property.GetValue(item, null) };
            var dict = new Dictionary<string, object>();
            foreach (var prop in props)
            {
                dict.Add(prop.Name, prop.Value);
            }
            return dict;
        }

    }
}
