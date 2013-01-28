using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.Domain;
using DRCOG.Common.DataInterfaces;

namespace DRCOG.Common.Interfaces.QueueSupport
{
    public interface IChangeService : IChangeTrackerRepository
    {
        //Dictionary<Guid, IList<PropertyChange>> GetChangeRecordsByChange(Guid id);
        //Dictionary<Guid, IList<PropertyChange>> GetChangeRecordsByChange(int id);
        //bool InsertChangeRecords(Guid sId, Guid cId, IList<PropertyChange> changes);
    }
}
