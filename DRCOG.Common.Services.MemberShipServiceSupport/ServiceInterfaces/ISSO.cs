using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DRCOG.Common.Services.MemberShipServiceSupport;
using DRCOG.Common.Services.MemberShipServiceSupport.Interfaces;
using DRCOG.Common.Services.MemberShipServiceSupport.Domain;
using DRCOG.Common.Service.MemberShipServiceSupport.Domain.Search;
using System.Collections;
using DRCOG.Common.Services.MemberShipServiceSupport.Exceptions;
using DRCOG.Common.Services.QueueSupport.QueueCommand;
using System.Web.Security;

namespace DRCOG.Common.Service.MemberShipServiceSupport.Interfaces
{
    [ServiceKnownType(typeof(PasswordResetResultType))]
    [ServiceKnownType(typeof(IList))]
    [ServiceKnownType(typeof(SSOUserAlreadyExistsException))]
    [ServiceKnownType(typeof(SSOUserDoesNotExistException))]
    [ServiceKnownType(typeof(SSOInvalidPasswordException))]
    [ServiceKnownType(typeof(SSOMembershipCreateResponse))]
    [ServiceKnownType(typeof(Profile))]
    [ServiceContract]
    public interface ISSO
    {
        [OperationContract]
        [FaultContract(typeof(SSOUserAlreadyExistsException))]
        MembershipCreateStatus CreateUser(string userName, string password, string eMail, bool isActive);
        [OperationContract]
        [FaultContract(typeof(SSOUserAlreadyExistsException))]
        SSOMembershipCreateResponse CreateUserWithProfile(Profile profile, bool isActive);
        [OperationContract]
        void DeleteUser(string userName);

        //[OperationContract]
        //bool UserExists(string userName);

        [OperationContract]
        bool ChangePassword(string userName, string oldPaassword, string newPassword);

        [OperationContract]
        IList<Profile> GetUsers(Guid userId);

        [OperationContract]
        bool SaveUserWithTracking(Profile model);//QueueChangesCommand<Profile, Guid> changesCommand);
        
        [OperationContract]
        void SaveUser(Profile person);

        [OperationContract]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        Profile GetUserByName(string userName, bool loadRoles);

        [OperationContract]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        QueueChangesCommand<Profile, Guid> GetUserWithTracking(Guid userId);

        [OperationContract(Name = "GetUserWithTrackingFilteredByUserName")]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        QueueChangesCommand<Profile, Guid> GetUserWithTracking(string userName);

        [OperationContract]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        Profile GetADUserByName(string userName, bool loadRoles);

        [OperationContract]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        Profile GetUserByEmail(string emailAddress, bool loadRoles);

        [OperationContract]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        Profile GetUserByID(int id, bool loadRoles);

        [OperationContract]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        Profile GetUserByGuid(Guid id, bool loadRoles);

        // *** USER SERVICES ***
        //Membership - User
        //[OperationContract]
        //List<MembershipUser> SearchMembershipUsers(CriteriaSearchSpecificationSettings specSettings);
        [OperationContract]
        List<Profile> GetMembershipUsers(Guid userId);
        //[OperationContract]
        //MembershipUser GetMembershipUser(Guid userID);
        [OperationContract]
        [FaultContract(typeof(SSOInvalidPasswordException))]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        ValidateUserResultType ValidateUser(string userName, string password);

        [OperationContract]
        [FaultContract(typeof(SSOInvalidPasswordException))]
        [FaultContract(typeof(SSOUserDoesNotExistException))]
        ValidateUserResultType ValidateADUser(string userName, string password);

        //[OperationContract]
        //void SignOut(string userName);

        //[OperationContract]
        //bool IsAuthenticated(string userName);

        [OperationContract]
        PasswordResetResultType ResetPassword(string emailAddress, string password);

        [OperationContract]
        int GetOnlineCount();

        //[OperationContract]
        //void UpdateUser(string userName, string password, string eMail, bool isActive);


        //[OperationContract]
        //Profile GetUserProfile(string userName);
        //[OperationContract]
        //void SaveUserProfile(Profile person);

        //Membership - Application
        //[OperationContract]
        //List<MembershipApplication> GetMembershipApplications();
        //[OperationContract]
        //MembershipApplication GetMembershipApplication(Guid applicationID);

        //Membership - Profile
        //[OperationContract]
        //MembershipProfile GetMembershipProfile(Guid userID);
        [OperationContract]
        List<ProfilePropertyValue> GetProfilePropertyValues(Guid userID);
        [OperationContract]
        List<ProfileProperty> GetProfileProperties();
        [OperationContract(Name="GetProfilePropertiesFilteredByUser")]
        List<ProfileProperty> GetProfileProperties(Guid userID);
        [OperationContract]
        int CreateProfileProperty(string propertyName, out string Response);
        [OperationContract]
        int SaveProfilePropertyValue(Guid userID, int propertyID, string propertyValue, out string retPropertyName, out string retPropertyValue, out int retPropertyID);
        [OperationContract]
        bool SaveProfilePropertyValues(Guid userId, List<ProfilePropertyValue> properties);
        [OperationContract]
        bool UpdateProfilePropertyValue(Guid userID, int propertyID, string propertyValue);
        [OperationContract]
        void DeleteProfilePropertyValue(int profilePropertyValueID);

        [OperationContract]
        Dictionary<string, Dictionary<string, bool>> GetRolesForUser(string userName);

        [OperationContract]
        void AddUserToRole(string userName, string roleName, RoleProviderType providerType);

        [OperationContract]
        void RemoveUserFromRole(string userName, string roleName, RoleProviderType providerType);
    }
}
