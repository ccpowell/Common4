using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.Interfaces.QueueSupport;
using DRCOG.Common.DataInterfaces;
using DRCOG.Common.Domain;

namespace DRCOG.Common.Services.ChangeTrackerSupport
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
            throw new NotImplementedException();
        }

        public bool InsertChangeRecords(Guid bindingId, Guid sId, Guid cId, IList<PropertyChange> changes)
        {
            return _changeTrackerRepository.InsertChangeRecords(bindingId, sId, cId, changes);
        }

        public Dictionary<Guid, IList<PropertyChange>> GetChangeRecordsByChangeContact(int id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Guid, IList<PropertyChange>> GetChangeRecordsByChangeContact(Guid id)
        {
            return _changeTrackerRepository.GetChangeRecordsByChangeContact(id);
        }
    }
}
