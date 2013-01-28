using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DRCOG.Common.Services.MemberShipServiceSupport.Domain
{
    [DataContract]
    public class MembershipApplication
    {
        [DataMember]
        public string ApplicationName { get; set; }
        [DataMember]
        public string LoweredApplicationName { get; set; }
        [DataMember]
        public Guid ApplicationID { get; set; }
        [DataMember]
        public string Description { get; set; }

        public MembershipApplication()
        {
        }
    }
}
