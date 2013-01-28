using System;
using System.Runtime.Serialization;
using DRCOG.Common.Services.MemberShipServiceSupport.Interfaces;

namespace DRCOG.Common.Service.MemberShipServiceSupport.Domain.Search
{
    [DataContract]
    public class CriteriaSearchSpecificationSettings : ICriteriaSearchSpecificationSettings
    {
        [DataMember]
        public string ApplicationName { get; set; }
        [DataMember]
        public Guid ApplicationGuid { get; set; }

        [DataMember]
        public string SystemName { get; set; }
        [DataMember]
        public int SystemID { get; set; }

        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
    }
}
