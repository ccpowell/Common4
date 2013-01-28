using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Domain
{
    public abstract class Change
    {
        //private object _newValue;
        private object _oldValue;

        public Change(object newValue, object oldValue)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }

        public readonly object NewValue;// { get; protected set; }
        public object OldValue { get; set; }
    }

    public class PropertyChange : Change, IEquatable<PropertyChange>
    {
        public readonly string PropertyName;

        public PropertyChange(string propertyName, object newValue, object oldValue)
            : base(newValue, oldValue)
        {
            this.PropertyName = propertyName;
        }


        public bool Equals(PropertyChange other)
        {
            if (this.PropertyName == other.PropertyName)
            {
                return true;
            }
            else { return false; }
        }
    }
}
