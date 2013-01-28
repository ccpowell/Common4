using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.Interfaces.QueueSupport;
using DRCOG.Common.Services.MemberShipServiceSupport;
using System.Web.Security;
using DRCOG.Common.Services.MemberShipServiceSupport.Domain;

namespace DRCOG.Common.Service.MemberShipServiceSupport.Interfaces
{
    public interface IProfileService : IChangeService
    {
        void Load(ref Profile person, MembershipProvider provider);
        void SaveProfile(Profile person);
        void Save(Profile person, MembershipProviderType providerName);

        Profile GetUserProfile(string userName, MembershipProviderType providerName);
        Profile GetUserProfile(Guid userId, MembershipProviderType providerName);

        //string[] GetRolesForUser(string userName, Enums.RoleProvider providerType);
        Dictionary<string, bool> GetRolesForUser(string userName, RoleProvider provider);
        Dictionary<string, bool> GetRolesForUser(string userName, RoleProviderType providerName);

        List<ProfilePropertyValue> GetProfilePropertyValues(Guid userID);
        List<ProfileProperty> GetProfileProperties();
        List<ProfileProperty> GetProfileProperties(Guid userID);
        int CreateProfileProperty(string propertyName, out string Response);
        int CreateProfileProperty(string propertyName, ProfilePropertyType type, out string Response);
        int SaveProfilePropertyValue(Guid userID, int propertyID, string propertyValue, out string PropertyName, out string PropertyValue, out int PropertyID);
        bool SaveProfilePropertyValues(Guid userId, List<ProfilePropertyValue> properties);
        bool UpdateProfilePropertyValue(Guid userID, int propertyID, string propertyValue);
        void DeleteProfilePropertyValue(int profilePropertyValueID);

        Guid GetProfilePropertyUserGuid_ByValue(string propertyValue, string propertyName);
        Guid GetProfilePropertyUserGuid_ByValue(string propertyValue, int propertyId);
        Guid GetProfilePropertyUserGuid_ByValue(string propertyValue, string propertyName, int propertyId);
    }
}
