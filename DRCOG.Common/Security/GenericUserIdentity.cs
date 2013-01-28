using System;
using DRCOG.Common.DesignByContract;
using System.Collections.Generic;

namespace DRCOG.Common.Security
{
    /// <summary>
    /// Generic User identity meant to work with user information pulled from Active Directory.
    /// </summary>
    /// <typeparam name="TId">The database identifier of the user.</typeparam>
    public class GenericUserIdentity<TId> : IGenericUserIdentity<TId>
    {
        #region IIdentity Members

        public GenericUserIdentity(Boolean isAuthenticated)
        {
            IsAuthenticated = isAuthenticated;
        }

        public GenericUserIdentity(Boolean isAuthenticated, IDictionary<ActiveDirectoryAttribute, String> adUser, TId id)
        {
            ActiveDirectoryName = adUser[ActiveDirectoryAttribute.UserPrincipalName];
            Name = adUser[ActiveDirectoryAttribute.DisplayName];
            if (String.IsNullOrEmpty(Name) || !Name.Contains(","))
            {
                IsAuthenticated = false;
            }
            else
            {
                IsAuthenticated = isAuthenticated;
                IsStandardAccount = true;
            }
            Id = id;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName +
                ActiveDirectoryName +
                Id + IsAuthenticated).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }

            return GetHashCode().Equals(obj.GetHashCode());
        }

        private const String AUTHENTICATION_TYPE = "Active Directory";
        public string AuthenticationType
        {
            get { return AUTHENTICATION_TYPE; }
        }

        public bool IsAuthenticated
        {
            get;
            protected set;
        }

        public string Name
        {
            get;
            protected set;
        }

        public TId Id
        {
            get;
            protected set;
        }

        public bool IsStandardAccount
        {
            get; protected set;
        }

        public string ActiveDirectoryName
        {
            get;
            protected set;
        }

        public String FullName
        {
            get
            {
                if (String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName))
                {
                    return "";
                }
                return FirstName + " " + LastName;
            }
        }

        private String _firstName;
        public string FirstName
        {
            get
            {
                if (!String.IsNullOrEmpty(Name))
                {
                    if (String.IsNullOrEmpty(_firstName))
                    {
                        Check.Ensure(Name.Contains(","), "The Identity Name '" + Name + "' is not in the format 'LAST, FIRST'");
                        _firstName = Name.Split(',')[1].Trim();
                    }
                }
                return _firstName;
            }
        }

        private String _lastName;
        public string LastName
        {
            get
            {
                if (!String.IsNullOrEmpty(Name))
                {
                    if (String.IsNullOrEmpty(_lastName))
                    {
                        Check.Ensure(Name.Contains(","), "The Identity Name '" + Name + "' is not in the format 'LAST, FIRST'");
                        _lastName = Name.Split(',')[0].Trim();
                    }
                }
                return _lastName;
            }
        }

        #endregion
    }
}
