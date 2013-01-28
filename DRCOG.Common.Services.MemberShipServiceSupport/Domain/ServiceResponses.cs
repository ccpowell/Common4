using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Security;
using DRCOG.Common.Util;

namespace DRCOG.Common.Services.MemberShipServiceSupport.Domain
{
    [Serializable]
    [DataContract]
    public class SSOMembershipCreateResponse
    {
        private string _status = "";
        private string _userName = "";
        public SSOMembershipCreateResponse(MembershipCreateStatus status, string userName)
        {
            _status = status.ToString();
            _userName = userName;
        }
        [DataMember]
        public string Status
        {
            get
            {
                return _status;
            }
            private set { _status = value; }
        }
        [DataMember]
        public string UserName
        {
            get
            {
                return _userName;
            }
            private set { _userName = value; }
        }

        public MembershipCreateStatus StatusEnum
        {
            get
            {
                return this.Status.ParseAsEnum<MembershipCreateStatus>(true);
            }
        }
    }
}
