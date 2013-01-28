using System.Runtime.Serialization;
using System;

namespace DRCOG.Common.Service.MemberShipServiceSupport.Domain.Search
{
    public class SearchCriteria
    {
        public string ApplicationName { get; set; }
        public Guid ApplicationID { get; set; }
        public string SystemName { get; set; }
        public int SystemID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public SearchCriteria()
        {
        }
    }
}
