using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DRCOG.Common.Services.MemberShipServiceSupport;
using DRCOG.Common.Services.MemberShipServiceSupport.Interfaces;
using DRCOG.Common.Services.MemberShipServiceSupport.Domain;
using DRCOG.Common.Service.MemberShipServiceSupport.Domain.Search;

namespace DRCOG.Common.Service.MemberShipServiceSupport.Interfaces
{
    [ServiceContract]
    public interface IGlobalUserService
    {
        // *** USER SERVICES ***
        //Membership - User
        //[OperationContract]
        //List<MembershipUser> SearchMembershipUsers(CriteriaSearchSpecificationSettings specSettings);
        [OperationContract]
        List<Profile> GetMembershipUsers(string membershipApplication);
        //[OperationContract]
        //MembershipUser GetMembershipUser(Guid userID);
        [OperationContract]
        bool ValidateUser(string userName, string password);
        [OperationContract]
        Profile GetUserProfile(string userName);
        [OperationContract]
        void SaveUserProfile(Profile person);

        //Membership - Application
        [OperationContract]
        List<MembershipApplication> GetMembershipApplications();
        [OperationContract]
        MembershipApplication GetMembershipApplication(Guid applicationID);

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
        int SaveProfilePropertyValue(Guid userID, int propertyID, string propertyValue, out string PropertyName, out string PropertyValue, out int PropertyID);
        [OperationContract]
        bool SaveProfilePropertyValues(Guid userId, List<ProfilePropertyValue> properties);
        [OperationContract]
        bool UpdateProfilePropertyValue(Guid userID, int propertyID, string propertyValue);
        [OperationContract]
        void DeleteProfilePropertyValue(int profilePropertyValueID);
    }
}
