using System;
using System.Runtime.Serialization;

namespace DRCOG.Common.Services.MemberShipServiceSupport.Domain
{
    [DataContract]
    public class MembershipUser
    {
        [DataMember]
        public Guid UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }

        public MembershipUser()
        {
        }

        public MembershipUser(string userName)
        {
            UserID = System.Guid.Empty;
            UserName = userName;
        }

        public MembershipUser(string userName, Guid userID)
        {
            UserID = userID;
            UserName = userName;
        }
    }
}