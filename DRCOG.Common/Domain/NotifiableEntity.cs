using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.CustomEvents;
using DRCOG.Common.DesignByContract;
using System.Runtime.Serialization;
using System.Reflection;
using System.ComponentModel;

namespace DRCOG.Common.Domain
{
    [DataContract]
    public abstract class NotifiableEntity<IdT> : BaseEntity<IdT>, INotifyPropertyChange
    {
        public bool IsDirty { get; protected set; }

        public NotifiableEntity()
        {
            this.IsDirty = false;
        }

        public void FinalizeAndQueue()
        {
            // only finalize if there are changes
            Check.Ensure(IsDirty);
            NotifyProperyChange("FinalizeAndQueue", null, null);
        }

        public delegate bool Commit();

        public event PropertyChangeEventHandler PropertyChange;

        protected virtual void NotifyProperyChange(string propertyName, object oldValue, object newValue)
        {
            if (this.PropertyChange != null)
            {
                if (propertyName != "FinalizeAndQueue") this.IsDirty = true;
                this.PropertyChange.Invoke(this, new PropertyChangeEventArgs(propertyName, oldValue, newValue));
            }
        }

        protected virtual void NotifyProperyChange(string propertyName)
        {
            if (this.PropertyChange != null)
            {
                this.PropertyChange.Invoke(this, new PropertyChangeEventArgs(propertyName));
            }
        }
    }
}
