using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.ServiceInterfaces.QueueSupport;
using DRCOG.Common.DataInterfaces;
using DRCOG.Common.Domain;

namespace DRCOG.Common.Services.QueueSupport
{
    public class ChangeService : IChangeService
    {
        IChangeTrackerRepository _changeTrackerRepository;

        public ChangeService(IChangeTrackerRepository changeTrackerRepository)
        {
            _changeTrackerRepository = changeTrackerRepository;
        }

        public bool InsertChangeRecords(Guid sId, Guid cId, IList<PropertyChange> changes)
        {
            return _changeTrackerRepository.InsertChangeRecords(sId, cId, changes);
        }

        public Dictionary<Guid, IList<PropertyChange>> GetChangeRecordsByChange(int id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Guid, IList<PropertyChange>> GetChangeRecordsByChange(Guid id)
        {
            return _changeTrackerRepository.GetChangeRecordsByChangeContact(id);
        }
    }
}
