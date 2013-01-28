using System;
namespace DRCOG.Common.Services.MemberShipServiceSupport.Interfaces
{
    public interface IRoleService
    {
        bool RoleExists(string roleName);
        bool IsUserInRole(string userName, string roleName);
        void CreateRole(string roleName);
        bool DeleteRole(string roleName);
        bool DeleteRole(string roleName, bool throwOnPopulatedRole);
    }
}
