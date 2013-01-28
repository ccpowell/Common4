using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.Services.QueueSupport.QueueCommand;
using DRCOG.Common.Services.MemberShipServiceSupport.Domain;
using System.Web.Security;

namespace DRCOG.Common.Services.MemberShipServiceSupport.Interfaces
{
    public interface ISSOFederation
    {
        MembershipCreateStatus CreateUser(string userName, string password, string eMail, bool isActive);
        SSOMembershipCreateResponse CreateUserWithProfile(Profile profile, bool isActive);
        MembershipCreateStatus CreateUserWithProfile(Profile profile, bool isActive, string welcomeMsg, string fromEmail);

        void DeleteUser(string userName);
        //bool UserExists(string userName);
        //void UpdateUser(string userName, string password, string eMail, bool isActive);
        bool SaveUserWithTracking(Profile model);//QueueChangesCommand<Profile, Guid> changesCommand);
        void SaveUser(Profile person);

        bool ChangePassword(string userName, string oldPaassword, string newPassword);
        PasswordResetResult ResetPassword(string emailAddress);
        PasswordResetResultType ResetPassword(string emailAddress, string password);
        //string RetrievePassword(string userName);

        IList<Profile> FindUsersByEmail(string EmailAddress);
        IList<Profile> FindUsersByName(string UserName);
        IList<Profile> GetUsers(Guid userId);
        //IList<Profile> GetUsers(int index, int count);
        IList<Profile> GetUsers(Guid userId, bool includeAllUsers);
        IList<Profile> GetUsers(Guid userId, string[] applications);

        Dictionary<string, bool> GetRolesForUser(string userName, RoleProviderType providerType);
        Dictionary<string, bool> GetRolesForUser(string userName, RoleProvider provider);
        Dictionary<string, Dictionary<string, bool>> GetRolesForUser(string userName);
        void AddUserToRole(string userName, string roleName, RoleProviderType providerType);
        void RemoveUserFromRole(string userName, string roleName, RoleProviderType providerType);

        int GetOnlineUsersCount();

        QueueChangesCommand<Profile, Guid> GetUserWithTracking(string userName);
        QueueChangesCommand<Profile, Guid> GetUserWithTracking(Guid userId);
        Profile GetUserByName(string userName, bool loadRoles);
        Profile GetADUserByName(string userName, bool loadRoles);
        Profile GetUserByEmail(string emailAddress, bool loadRoles);
        Profile GetUserByID(int id, bool loadRoles);
        Profile GetUserByID(Guid id, bool loadRoles);

        ValidateUserResultType ValidateUser(string userName, string password);
        ValidateUserResultType ValidateADUser(string userName, string password);
        //void SignOut(string userName);
        //bool IsAuthenticated(string userName);

        List<Profile> GetMembershipUsers(Guid userId);
        List<MembershipApplication> GetMembershipApplications();
        MembershipApplication GetMembershipApplication(Guid applicationID);
        List<string> GetMembershipUserApplications(Guid membershipUserId, bool includeMembershipApp);
        
        List<ProfilePropertyValue> GetProfilePropertyValues(Guid userID);
        List<ProfileProperty> GetProfileProperties();
        List<ProfileProperty> GetProfileProperties(Guid userID);
        int CreateProfileProperty(string propertyName, out string Response);
        int CreateProfileProperty(string propertyName, ProfilePropertyType type, out string Response);
        int SaveProfilePropertyValue(Guid userID, int propertyID, string propertyValue, out string PropertyName, out string PropertyValue, out int PropertyID);
        bool SaveProfilePropertyValues(Guid userId, List<ProfilePropertyValue> properties);
        bool UpdateProfilePropertyValue(Guid userID, int propertyID, string propertyValue);
        void DeleteProfilePropertyValue(int profilePropertyValueID);

        void UpdateUserApproval(string username, bool isApproved);
        void UpdateUserApproval(Guid userId, bool isApproved);
        void UnlockUser(string username);

    }
}
