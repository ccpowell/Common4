using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using DRCOG.Common.Services.MemberShipServiceSupport.Interfaces;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    public class RoleService : IRoleService
    {
        private RoleProvider RoleProvider;

        public RoleService(RoleProviderType providerType)
        {
            RoleProvider = RoleProviderFactory.GetProvider(providerType);
        }

        public bool RoleExists(string roleName)
        {
            return this.RoleProvider.RoleExists(roleName);
        }

        public bool IsUserInRole(string userName, string roleName)
        {
            return this.RoleProvider.IsUserInRole(userName, roleName);
        }

        public void CreateRole(string roleName)
        {
            this.RoleProvider.CreateRole(roleName);
            
        }

        public bool DeleteRole(string roleName)
        {
            return this.DeleteRole(roleName, true);
        }

        public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return this.RoleProvider.DeleteRole(roleName, throwOnPopulatedRole);
        }
    }
}
