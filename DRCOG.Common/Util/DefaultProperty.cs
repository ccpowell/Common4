using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DRCOG.Common.Util
{
    public static class DefaultProperty
    {
        public static object GetDefault<T>(this T obj) where T : PropertyInfo
        {
            Type type = obj.PropertyType;
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;


        }
    }
}
