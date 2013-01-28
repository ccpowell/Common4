using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.ComponentModel;
using DRCOG.Common.CustomEvents;
using System.Collections.ObjectModel;
using System.Reflection;
using DRCOG.Common.Util;

namespace DRCOG.Common.Domain
{
    public class PropertyChangeWatcher<T, IdT> where T : NotifiableEntity<IdT>
    {
        public delegate void WatchForChangeDelegate(object sender, PropertyChangeEventArgs args);

        private readonly List<PropertyChange> _changeLog;
        private T _entity;

        public T Entity
        {
            get { return _entity; }
            set
            {
                _entity = value;
            }
        }

        public PropertyChangeWatcher()
            : this((T)Activator.CreateInstance(typeof(T)))
        {
            
            //Entity = (T)Activator.CreateInstance(typeof(T));
        }

        public PropertyChangeWatcher(T model)
        {
            this._changeLog = new List<PropertyChange>();
            this.Entity = model;
            
        }

        public PropertyChangeWatcher(WatchForChangeDelegate watcher)
            : this() 
        {
            this.StartWatching(watcher);
        }
        public PropertyChangeWatcher(T model, WatchForChangeDelegate watcher)
            : this(model) 
        {
            this.StartWatching(watcher);
        }

        public void StartWatching()
        {
            StartWatching(WatchForChange);
        }

        public void StartWatching(WatchForChangeDelegate watcher)
        {
            var listener = ChangeListener<IdT>.Create(Entity);
            listener.PropertyChange +=
                new PropertyChangeEventHandler(watcher);
        }

        void WatchForChange(object sender, PropertyChangeEventArgs args)
        {
            PropertyChange change = new PropertyChange(args.PropertyName, args.NewValue, args.OldValue);
            if (!_changeLog.Contains(change))
            {
                _changeLog.Add(change);
            }
            else
            {
                var item = _changeLog.Find(x => x.PropertyName == args.PropertyName);
                var index = _changeLog.FindIndex(x => x.PropertyName == args.PropertyName);
                change.OldValue = item.OldValue;
                _changeLog[index] = change;
            }
        }

        public ReadOnlyCollection<PropertyChange> ChangeLog
        {
            get
            {
                if (this.Entity.IsTransient())
                {
                    this._changeLog.AddRange(new TransientChanges<T, IdT>(this.Entity).ChangeLog);
                }
                return this._changeLog.AsReadOnly();
            }
        }

        public void AddChange(PropertyChange change)
        {
            this._changeLog.Add(change);
        }

        public void UpdateChange(PropertyChangeEventArgs args)
        {
            PropertyChange change = new PropertyChange(args.PropertyName, args.NewValue, args.OldValue);

            var item = _changeLog.Find(x => x.PropertyName == args.PropertyName);
            var index = _changeLog.FindIndex(x => x.PropertyName == args.PropertyName);
            change.OldValue = item.OldValue;
            _changeLog[index] = change;
        }

        
    }
}
