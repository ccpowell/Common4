using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DRCOG.Common.CustomEvents
{
    public class PropertyChangeEventArgs : PropertyChangedEventArgs
    {
        public object OldValue { get; internal set; }
        public object NewValue { get; internal set; }

        public PropertyChangeEventArgs(string propertyName, object oldValue, object newValue)
            : base(propertyName)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public PropertyChangeEventArgs(string propertyName)
            : base(propertyName)
        { }
    }
}
