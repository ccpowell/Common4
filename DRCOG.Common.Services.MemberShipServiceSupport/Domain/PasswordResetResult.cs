using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Services.MemberShipServiceSupport.Domain
{
    public class PasswordResetResult
    {
        public PasswordResetResultType PasswordResetResultType { get; set; }
        public string Password { get; set; }

        public PasswordResetResult() { }
        public PasswordResetResult(PasswordResetResultType passwordResetResultType)
        {
            this.PasswordResetResultType = passwordResetResultType;
        }

        public PasswordResetResult(PasswordResetResultType passwordResetResultType, string password)
            : this(passwordResetResultType)
        {
            this.Password = password;
        }


    }
}
