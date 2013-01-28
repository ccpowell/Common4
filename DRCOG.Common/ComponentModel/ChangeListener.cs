using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.CustomEvents;
using System.Collections.Specialized;
using DRCOG.Common.Domain;

namespace DRCOG.Common.ComponentModel
{
    public abstract class ChangeListener<IdT> : NotifiableEntity<IdT>, IDisposable
    {
        #region *** Members ***
        protected string _propertyName;
        #endregion


        #region *** Abstract Members ***
        protected abstract void Unsubscribe();
        #endregion


        #region *** INotifyPropertyChange Members and Invoker ***
        //public event PropertyChangeEventHandler PropertyChange;

        //protected virtual void RaisePropertyChanged(string propertyName, object oldValue, object newValue)
        //{
        //    NotifiableEntity.PropertyChange +=
        //        new PropertyChangeEventHandler(

        //    var temp = PropertyChange;
        //    if (temp != null)
        //        temp(this, new PropertyChangeEventArgs(propertyName, oldValue, newValue));
        //}
        #endregion


        #region *** Disposable Pattern ***

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Unsubscribe();
            }
        }

        ~ChangeListener()
        {
            Dispose(false);
        }

        #endregion


        #region *** Factory ***
        public static ChangeListener<IdT> Create(NotifiableEntity<IdT> value)
        {
            return Create(value, null);
        }

        public static ChangeListener<IdT> Create(NotifiableEntity<IdT> value, string propertyName)
        {
            if (value is INotifyCollectionChanged)
            {
                return new CollectionChangeListener<IdT>(value as INotifyCollectionChanged, propertyName);
            }
            else if (value is INotifyPropertyChange)
            {
                return new ChildChangeListener<IdT>(value as INotifyPropertyChange, propertyName);
            }
            else
                return null;
        }
        #endregion
    }
}
