using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DRCOG.Common.CustomEvents;
using DRCOG.Common.Services.QueueSupport.QueueCommand;
using DRCOG.Common.Domain;
using DRCOG.Common.Domain.Attributes;
using System.Collections;
using DRCOG.Common.Interfaces;

namespace DRCOG.Common.Services.QueueSupport.Util
{
    public static class ObjectHelper<T, IdT> where T : NotifiableEntity<IdT>, IProperty
    {
        /// <summary>
        /// Compare and Update object
        /// </summary>
        /// <param name="x">object change wrapper</param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static N CompareAndUpdate<N>(N x, T y) where N : IQueueChangesCommand<T, IdT> 
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreOnUpdateAttribute)))
                .ToArray();//BindingFlags.DeclaredOnly | BindingFlags.Public);
            FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public);
            int compareValue = 0;
            bool compareResult = true;

            foreach (PropertyInfo property in properties)
            {
                Type pType = property.PropertyType;
                if (pType.IsGenericType && pType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var pY = property.GetValue(y, null);
                    foreach (var item in (pY as IList))
                    {
                        if(item is IPropertyValue)
                        {
                            var i = item as IPropertyValue;
                            x.Entity.SetValue(i.PropertyName, i.PropertyValue);
                            continue;
                        }
                    }

                    continue;
                }

                IComparable valx = property.GetValue(x.Entity, null) as IComparable;
                if (valx == null)
                    continue;
                object valy = property.GetValue(y, null);
                if (valy == null || valy.Equals("PropertyNotFound"))
                    continue;

                compareValue = valx.CompareTo(valy);
                if (compareValue != 0)
                {
                    property.SetValue(x.Entity, valy, null);
                    compareResult = false;
                }
                    //return compareValue;
            }
            foreach (FieldInfo field in fields)
            {
                IComparable valx = field.GetValue(x) as IComparable;
                if (valx == null)
                    continue;
                object valy = field.GetValue(y);
                compareValue = valx.CompareTo(valy);
                //if (compareValue != 0)
                    //return compareValue;
            }

            return x;
        }
    }
}
