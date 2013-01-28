using System;
using System.Collections.Generic;
using DRCOG.Common.Services.MemberShipServiceSupport.Interfaces;
using DRCOG.Common.Services.MemberShipServiceSupport;
using DRCOG.Common.Services.MemberShipServiceSupport.Domain;

namespace DRCOG.Common.Service.MemberShipServiceSupport.Interfaces
{
    public interface IUserRepository
    {
        //Membership - User
        //List<MembershipUser> SearchMembershipUsers(ICriteriaSearchSpecificationSettings specSettings);
        //List<Profile> GetMembershipUsers(Enums.MembershipProvider providerName, IProfileService profileService);
        //List<Profile> GetMembershipUsers(Enums.MembershipProvider providerName, IProfileService profileService, int index, int count);

        List<Profile> GetMembershipUsers(Guid membershipUserId, string[] applications, bool includeMembershipApp);
        List<Profile> GetMembershipUsers(Guid membershipUserId, string[] applications, string[] roleIsolators, bool includeMembershipApp);
        List<Profile> GetMembershipUsers(Guid membershipUserId, Dictionary<String, Object> parameters, bool includeMembershipApp);
        List<Profile> GetMembershipUsers(Guid membershipUserId);
        
        
        void RebuildUserCache();
        void RefreshUserInCache(string userName);
        void DeleteUserFromCache(string userName);
        //MembershipUser GetMembershipUser(Guid userID);

        //Membership - Application
        List<MembershipApplication> GetMembershipApplications();
        MembershipApplication GetMembershipApplication(Guid applicationID);
        List<string> GetMembershipUserApplications(Guid membershipUserId, bool includeMembershipApp);

        //Membership - Profile
        //MembershipProfile GetMembershipProfile(Guid userID);
        List<ProfilePropertyValue> GetProfilePropertyValues(Guid userID);
        List<ProfileProperty> GetProfileProperties();
        List<ProfileProperty> GetProfileProperties(Guid userID);
        int CreateProfileProperty(string propertyName, out string response);
        int CreateProfileProperty(string propertyName, ProfilePropertyType type, out string response);
        int SaveProfilePropertyValue(Guid userID, int propertyID, string propertyValue, out string propertyName);
        bool UpdateProfilePropertyValue(Guid userID, int propertyID, string propertyValue);
        void DeleteProfilePropertyValue(int profilePropertyValueID);

        Guid GetProfilePropertyUserGuid_ByValue(string propertyValue, string propertyName);
        Guid GetProfilePropertyUserGuid_ByValue(string propertyValue, int propertyId);
        Guid GetProfilePropertyUserGuid_ByValue(string propertyValue, string propertyName, int propertyId);

    }
}
