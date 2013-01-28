using System;
using DRCOG.Common.Domain;
using System.Collections.ObjectModel;
using DRCOG.Common.Interfaces;

namespace DRCOG.Common.Services.QueueSupport.QueueCommand
{
    public interface IQueueChangesCommand<T, IdT>
     where T : NotifiableEntity<IdT>, IProperty
    {
        ReadOnlyCollection<PropertyChange> ChangeLog { get; }
        T Entity { get; }
        PropertyChangeWatcher<T, IdT> watcher { get; }
    }
}
