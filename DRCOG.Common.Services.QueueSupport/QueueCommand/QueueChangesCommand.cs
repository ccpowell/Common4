using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DRCOG.Common.CustomEvents;
using DRCOG.Common.ComponentModel;
using DRCOG.Common.Interfaces.QueueSupport;
using DRCOG.Common.Domain;
using DRCOG.Common.Util;
using DRCOG.Common.DesignByContract;
using System.Runtime.Serialization;
using DRCOG.Common.Interfaces;

namespace DRCOG.Common.Services.QueueSupport.QueueCommand
{
    [DataContract]
    public abstract class QueueChangesCommand<T, IdT> : QueueCommand<T>, IQueueChangesCommand<T,IdT> where T : NotifiableEntity<IdT>, IProperty
    {
        protected IChangeService ChangeService;

        public PropertyChangeWatcher<T, IdT> watcher { get; private set; }
        public T Entity
        {
            get { return watcher.Entity; }
        }

        private void Init(IChangeService changeService)
        {
            this.ChangeService = changeService;
            StartWatching();
        }

        public QueueChangesCommand(QueueReceiver<T> receiver, IChangeService changeService)
            : base(receiver)
        {
            watcher = new PropertyChangeWatcher<T, IdT>();
            this.Init(changeService);
        }

        public QueueChangesCommand(QueueReceiver<T> receiver, IChangeService changeService, T model)
            : base(receiver)//, model)
        {
            watcher = new PropertyChangeWatcher<T, IdT>(model);
            this.Init(changeService);
        }

        private void StartWatching()
        {
            watcher.StartWatching(WatchForEnqueue);
        }

        private void WatchForEnqueue(object sender, PropertyChangeEventArgs args)
        {
            if (args.NewValue == null)
            {
                if (args.PropertyName.Equals("FinalizeAndQueue"))
                {
                    _receiver.Enqueue(this);
                }
            }
            else
            {
                PropertyChange change = new PropertyChange(args.PropertyName, args.NewValue, args.OldValue);
                if (!watcher.ChangeLog.Contains(change))
                {
                    watcher.AddChange(change);
                }
                else
                {
                    watcher.UpdateChange(args);
                }
            }
        }

        public ReadOnlyCollection<PropertyChange> ChangeLog
        {
            get
            {
                return watcher.ChangeLog;
            }
        }

    }
}
