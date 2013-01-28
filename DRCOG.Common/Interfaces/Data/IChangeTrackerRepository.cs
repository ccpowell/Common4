using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.Domain;

namespace DRCOG.Common.DataInterfaces
{
    public interface IChangeTrackerRepository
    {
        /// <summary>
        /// Insert a change record
        /// </summary>
        /// <param name="sId">SessionId identifies a batch of changes</param>
        /// <param name="cId">ChangeId groups many entity changes together</param>
        /// <param name="changes"></param>
        /// <returns></returns>
        [Obsolete("Use bool InsertChangeRecords(Guid userId, Guid sId, Guid cId, IList<PropertyChange> changes)", true)]
        bool InsertChangeRecords(Guid sId, Guid cId, IList<PropertyChange> changes);

        /// <summary>
        /// Insert a change record bound to a master record
        /// i.e. bindingId could be a user guid
        /// </summary>
        /// <param name="bindingId">uniqueid for lookup binding</param>
        /// <param name="sId">SessionId identifies a batch of changes</param>
        /// <param name="cId">ChangeId groups many entity changes together</param>
        /// <param name="changes"></param>
        /// <returns></returns>
        bool InsertChangeRecords(Guid bindingId, Guid sId, Guid cId, IList<PropertyChange> changes);

        Dictionary<Guid, IList<PropertyChange>> GetChangeRecordsByChangeContact(int id);
        Dictionary<Guid, IList<PropertyChange>> GetChangeRecordsByChangeContact(Guid id);
    }
}
