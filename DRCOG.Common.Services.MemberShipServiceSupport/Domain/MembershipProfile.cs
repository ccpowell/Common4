using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DRCOG.Common.Services.MemberShipServiceSupport.Domain
{
    [DataContract]
    public class MembershipProfile
    {
        [DataMember]
        public Guid UserID { get; set; }
        [DataMember]
        public IDictionary<string, ProfilePropertyValue> Properties { get; private set; }

        public MembershipProfile()
        {
            Properties = new Dictionary<string, ProfilePropertyValue>();
        }
    }
}
