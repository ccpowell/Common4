using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DRCOG.Common.Domain
{
    public static class PropertyCache
    {
 
        private static IDictionary<Type, IEnumerable<PropertyInfo>> properties = new Dictionary<Type, IEnumerable<PropertyInfo>>();
 
        public static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
 
            if (!properties.ContainsKey(type))
                properties.Add(type, type.GetProperties());
 
            return properties[type];
 
        }
 
        public static IEnumerable<PropertyInfo> GetSignatureProperties(Type type)
        {
 
            return GetProperties(type).Where(property => property.IsDefined(typeof(SignatureAttribute), true));
 
        }
 
    }
 
}



