using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using DRCOG.Common.CustomEvents;
using System.Collections;
using DRCOG.Common.Domain;

namespace DRCOG.Common.ComponentModel
{
    public class CollectionChangeListener<IdT> : ChangeListener<IdT>
    {
        #region *** Members ***
        private readonly INotifyCollectionChanged _value;
        private readonly Dictionary<INotifyPropertyChange, ChangeListener<IdT>> _collectionListeners = new Dictionary<INotifyPropertyChange, ChangeListener<IdT>>();
        #endregion


        #region *** Constructors ***
        public CollectionChangeListener(INotifyCollectionChanged collection, string propertyName)
        {
            _value = collection;
            _propertyName = propertyName;

            Subscribe();
        }
        #endregion


        #region *** Private Methods ***
        private void Subscribe()
        {
            _value.CollectionChanged += new NotifyCollectionChangedEventHandler(value_CollectionChanged);

            foreach (INotifyPropertyChange item in (IEnumerable)_value)
            {
                ResetChildListener(item);
            }
        }

        private void ResetChildListener(INotifyPropertyChange item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            RemoveItem(item);

            ChangeListener<IdT> listener = null;

            // Add new
            if (item is INotifyCollectionChanged)
                listener = new CollectionChangeListener<IdT>(item as INotifyCollectionChanged, _propertyName);
            else
                listener = new ChildChangeListener<IdT>(item as INotifyPropertyChange);

            listener.PropertyChange += new PropertyChangeEventHandler(listener_PropertyChanged);
            _collectionListeners.Add(item, listener);
        }

        private void RemoveItem(INotifyPropertyChange item)
        {
            // Remove old
            if (_collectionListeners.ContainsKey(item))
            {
                _collectionListeners[item].PropertyChange -= new PropertyChangeEventHandler(listener_PropertyChanged);

                _collectionListeners[item].Dispose();
                _collectionListeners.Remove(item);
            }
        }


        private void ClearCollection()
        {
            foreach (var key in _collectionListeners.Keys)
            {
                _collectionListeners[key].Dispose();
            }

            _collectionListeners.Clear();
        }
        #endregion


        #region *** Event handlers ***
        void value_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                ClearCollection();
            }
            else
            {
                // Don't care about e.Action, if there are old items, Remove them...
                if (e.OldItems != null)
                {
                    foreach (INotifyPropertyChange item in (IEnumerable)e.OldItems)
                        RemoveItem(item);
                }

                // ...add new items as well
                if (e.NewItems != null)
                {
                    foreach (INotifyPropertyChange item in (IEnumerable)e.NewItems)
                        ResetChildListener(item);
                }
            }
        }


        void listener_PropertyChanged(object sender, PropertyChangeEventArgs e)
        {
            // ...then, notify about it
            NotifyProperyChange(string.Format("{0}{1}{2}",
                _propertyName, _propertyName != null ? "[]." : null, e.PropertyName), e.OldValue, e.NewValue);
        }
        #endregion


        #region *** Overrides ***
        /// <summary>
        /// Releases all collection item handlers and self handler
        /// </summary>
        protected override void Unsubscribe()
        {
            ClearCollection();

            _value.CollectionChanged -= new NotifyCollectionChangedEventHandler(value_CollectionChanged);

            System.Diagnostics.Debug.WriteLine("CollectionChangeListener unsubscribed");
        }
        #endregion
    }
}
