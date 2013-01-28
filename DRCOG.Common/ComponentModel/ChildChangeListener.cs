using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.CustomEvents;
using System.Collections.Specialized;
using System.Reflection;

namespace DRCOG.Common.ComponentModel
{
    public class ChildChangeListener<IdT> : ChangeListener<IdT>
    {
        #region *** Members ***
        protected static readonly Type _inotifyType = typeof(INotifyPropertyChange);

        private readonly INotifyPropertyChange _value;
        private readonly Type _type;
        private readonly Dictionary<string, ChangeListener<IdT>> _childListeners = new Dictionary<string, ChangeListener<IdT>>();
        #endregion


        #region *** Constructors ***
        public ChildChangeListener(INotifyPropertyChange instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");
            _value = instance;
            _type = _value.GetType();

            Subscribe();
        }

        public ChildChangeListener(INotifyPropertyChange instance, string propertyName)
            : this(instance)
        {
            _propertyName = propertyName;
        }
        #endregion


        #region *** Private Methods ***
        private void Subscribe()
        {
            _value.PropertyChange += new PropertyChangeEventHandler(value_PropertyChanged);

            var query =
                from property
                in _type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                where _inotifyType.IsAssignableFrom(property.PropertyType)
                select property;

            foreach (var property in query)
            {
                // Declare property as known "Child", then register it
                _childListeners.Add(property.Name, null);
                ResetChildListener(property.Name);
            }
        }


        /// <summary>
        /// Resets known (must exist in children collection) child event handlers
        /// </summary>
        /// <param name="propertyName">Name of known child property</param>
        private void ResetChildListener(string propertyName)
        {
            if (_childListeners.ContainsKey(propertyName))
            {
                // Unsubscribe if existing
                if (_childListeners[propertyName] != null)
                {
                    _childListeners[propertyName].PropertyChange -= new PropertyChangeEventHandler(child_PropertyChanged);

                    // Should unsubscribe all events
                    _childListeners[propertyName].Dispose();
                    _childListeners[propertyName] = null;
                }

                var property = _type.GetProperty(propertyName);
                if (property == null)
                    throw new InvalidOperationException(string.Format("Was unable to get '{0}' property information from Type '{1}'", propertyName, _type.Name));

                object newValue = property.GetValue(_value, null);

                // Only recreate if there is a new value
                if (newValue != null)
                {
                    if (newValue is INotifyCollectionChanged)
                    {
                        _childListeners[propertyName] =
                            new CollectionChangeListener<IdT>(newValue as INotifyCollectionChanged, propertyName);
                    }
                    else if (newValue is INotifyPropertyChange)
                    {
                        _childListeners[propertyName] =
                            new ChildChangeListener<IdT>(newValue as INotifyPropertyChange, propertyName);
                    }

                    if (_childListeners[propertyName] != null)
                        _childListeners[propertyName].PropertyChange += new PropertyChangeEventHandler(child_PropertyChanged);
                }
            }
        }
        #endregion


        #region *** Event Handler ***
        void child_PropertyChanged(object sender, PropertyChangeEventArgs e)
        {
            NotifyProperyChange(e.PropertyName, e.OldValue, e.NewValue);
        }

        void value_PropertyChanged(object sender, PropertyChangeEventArgs e)
        {
            // First, reset child on change, if required...
            ResetChildListener(e.PropertyName);

            // ...then, notify about it
            NotifyProperyChange(e.PropertyName, e.OldValue, e.NewValue);
        }

        //protected override void RaisePropertyChanged(string propertyName, object oldValue, object newValue)
        //{
        //    // Special Formatting
        //    base.RaisePropertyChanged(string.Format("{0}{1}{2}",
        //        _propertyName, _propertyName != null ? "." : null, propertyName), oldValue, newValue);
        //}
        #endregion


        #region *** Overrides ***
        /// <summary>
        /// Release all child handlers and self handler
        /// </summary>
        protected override void Unsubscribe()
        {
            _value.PropertyChange -= new PropertyChangeEventHandler(value_PropertyChanged);

            foreach (var binderKey in _childListeners.Keys)
            {
                if (_childListeners[binderKey] != null)
                    _childListeners[binderKey].Dispose();
            }

            _childListeners.Clear();

            System.Diagnostics.Debug.WriteLine("ChildChangeListener '{0}' unsubscribed", _propertyName);
        }
        #endregion
    }
}
