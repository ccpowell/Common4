using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Linq;

namespace DRCOG.Common.Security
{
    public class GenericUserPrincipal<TIdentity> : IGenericUserPrincipal<TIdentity> where TIdentity : IIdentity
    {
        public GenericUserPrincipal(IEnumerable<String> roles, TIdentity identity)
        {
            Roles = roles;
            UserIdentity = identity;
        }

        #region IPrincipal Members

        public IIdentity Identity
        {
            get
            {
                return UserIdentity;
            }
        }

        public TIdentity UserIdentity
        {
            get;
            private set;
        }

        private IEnumerable<String> Roles { get; set; }

        public bool IsInRole(string role)
        {
            if (Roles == null)
            {
                return false;
            }
            else
            {
                return Roles.Contains(role);
            }
        }

        #endregion
    }
}
