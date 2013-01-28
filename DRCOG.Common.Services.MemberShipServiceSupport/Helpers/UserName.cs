using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DRCOG.Common.Services.MemberShipServiceSupport.Helpers
{
    public enum UserNameOffsetTarget
    {
        FirstName
        ,
        LastName
    }

    public static class UserName
    {
        public static string Create(string firstName, string lastName, int offset, UserNameOffsetTarget target)
        {
            string userName = String.Empty;
            switch (target)
            {
                case UserNameOffsetTarget.FirstName:
                    userName = firstName.Substring(0, offset) + lastName;
                    break;
                case UserNameOffsetTarget.LastName:
                    userName = firstName + lastName.Substring(0, offset);
                    break;
            }

            return CleanUserName(userName.ToLower());
        }

        private static string CleanUserName(string userName)
        {
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(userName.Replace(" ", ""), String.Empty);
        }
    }
}
