using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Interfaces
{
    public interface IProperty
    {
        void SetValue(string propertyName, string value);
        string GetValue(string propertyName);

    }

    public interface IPropertyValue
    {
        int? ID { get; set; }
        int PropertyID { get; set; }
        string PropertyName { get; set; }
        string PropertyValue { get; set; }
    }
}
