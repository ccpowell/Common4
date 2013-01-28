using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DRCOG.Common.Services.MemberShipServiceSupport.Exceptions
{
    [Serializable]
    [DataContract]
    public class SSOUserAlreadyExistsException
    {
        private string errorMsg = "";
        public SSOUserAlreadyExistsException(string message)
        {
            errorMsg = message;
        }
        [DataMember]
        public string Message
        {
            get
            {
                return errorMsg;
            }
            private set { errorMsg = value; }
        }
    }

    [Serializable]
    [DataContract]
    public class SSOUserDoesNotExistException
    {
        private string errorMsg = "";
        public SSOUserDoesNotExistException(string message)
        {
            errorMsg = message;
        }
        [DataMember]
        public string Message
        {
            get
            {
                return errorMsg;
            }
            private set { errorMsg = value; }
        }
    }

    [Serializable]
    [DataContract]
    public class SSOInvalidPasswordException
    {
        private string errorMsg = "";
        public SSOInvalidPasswordException(string message)
        {
            errorMsg = message;
        }
        [DataMember]
        public string Message
        {
            get
            {
                return errorMsg;
            }
            private set { errorMsg = value; }
        }
    }
}
