using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using DRCOG.Common.Services.ChangeTrackerSupport;
using DRCOG.Common.DataInterfaces;
using DRCOG.Common.Service.MemberShipServiceSupport.Interfaces;
using System.Transactions;
using DRCOG.Common.Services.MemberShipServiceSupport.Domain;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    public class ProfileService : IProfileService
    {
        private IUserRepository UserRepository;

        public ProfileService(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        public Profile GetUserProfile(Guid userId, MembershipProviderType providerName)
        {
            Profile p = new Profile() { PersonGUID = userId };
            this.Load(ref p, MembershipProviderFactory.GetProvider(providerName));
            var profileProperties = this.GetProfilePropertyValues(p.PersonGUID);
            p.ProfileProperties = profileProperties;
            // load their roles
            //if (loadRoles)
            //    _GetRoles(ref p);
            //p.Roles = Roles.GetRolesForUser(p.UserName);

            return p;
        }

        public Profile GetUserProfile(string userName, MembershipProviderType providerName)
        {
            Profile p = new Profile() { UserName = userName };
            this.Load(ref p, MembershipProviderFactory.GetProvider(providerName));
            var profileProperties = this.GetProfilePropertyValues(p.PersonGUID);
            p.ProfileProperties = profileProperties;
            // load their roles
            //if (loadRoles)
            //    _GetRoles(ref p);
            //p.Roles = Roles.GetRolesForUser(userName);
            //p.RolesUnused = Roles.GetAllRoles().Except(p.Roles).ToArray();

            return p;
        }

        //private void _GetRoles(ref Profile profile)
        //{
        //    profile.Roles = Roles.GetRolesForUser(profile.UserName);
        //    profile.RolesUnused = Roles.GetAllRoles().Except(profile.Roles).ToArray();
        //}

        public Dictionary<string, bool> GetRolesForUser(string userName, RoleProviderType providerType)
        {
            var provider = RoleProviderFactory.GetProvider(providerType);

            return GetRolesForUser(userName, provider);
        }

        public Dictionary<string, bool> GetRolesForUser(string userName, RoleProvider provider)
        {
            Dictionary<string, bool> roles = new Dictionary<string, bool>();

            var rolesIn = provider.GetRolesForUser(userName);
            var rolesNotIn = provider.GetAllRoles().Except(rolesIn).ToArray();
            foreach (var role in rolesIn)
            {
                roles.Add(role, true);
            }

            foreach (var role in rolesNotIn)
            {
                roles.Add(role, false);
            }

            return roles;
        }


        public void Load(ref Profile person, MembershipProvider provider)
        {
            MembershipUser user = Membership.GetUser(person.UserName);

            person.PersonGUID = (Guid)user.ProviderUserKey;
            person.Comment = user.Comment;
            person.CreationDate = user.CreationDate;
            person.RecoveryEmail = user.Email;
            person.IsApproved = user.IsApproved;
            person.IsLockedOut = user.IsLockedOut;
            person.LastActivityDate = user.LastActivityDate;
            person.LastLockoutDate = user.LastLockoutDate;
            person.LastLoginDate = user.LastLoginDate;
            person.LastPasswordChangedDate = user.LastPasswordChangedDate;
        }

        //public void LoadWithMemberProfile(ref Profile person)
        //{
        //    MemberProfile profile = new MemberProfile();
        //    profile.Initialize(person.UserName, true);

        //    this.Load(ref person);
        //}

        public void Save(Profile person, MembershipProviderType providerName)
        {
            var provider = MembershipProviderFactory.GetProvider(providerName);
            MembershipUser user = provider.GetUser(person.UserName, false);
            user.Comment = person.Comment;
            user.Email = person.RecoveryEmail;
            user.IsApproved = person.IsApproved;

            this.SaveProfilePropertyValues(person.PersonGUID, person.ProfileProperties);

            provider.UpdateUser(user);
        }

        public void SaveProfile(Profile person)
        {
            MemberProfile profile = new MemberProfile();
            profile.Initialize(person.RecoveryEmail, true);

            profile.FirstName = person.FirstName;
            profile.LastName = person.LastName;
            profile.HomePhoneNumber = person.Phone;
            profile.HomeEmailAddress = person.HomeEmail;
            profile.HomeAddress = person.Address;
            profile.HomeCity = person.City;
            profile.HomeState = person.State;
            profile.HomeUnit = person.Unit;
            profile.Save();
        }

        #region Profile Properties

        public List<ProfilePropertyValue> GetProfilePropertyValues(Guid userID)
        {
            return UserRepository.GetProfilePropertyValues(userID);
        }

        public List<ProfileProperty> GetProfileProperties()
        {
            return UserRepository.GetProfileProperties();
        }

        public List<ProfileProperty> GetProfileProperties(Guid userID)
        {
            return UserRepository.GetProfileProperties(userID);
        }

        public int CreateProfileProperty(string propertyName, out string Response)
        {
            return this.CreateProfileProperty(propertyName, ProfilePropertyType.String, out Response);
        }

        public int CreateProfileProperty(string propertyName, ProfilePropertyType type, out string Response)
        {
            string responseString = "";
            int output;
            using (TransactionScope scope = new TransactionScope())
            {
                output = UserRepository.CreateProfileProperty(propertyName, type, out responseString);
                scope.Complete();
            }
            Response = responseString;
            return output;
        }

        public int SaveProfilePropertyValue(Guid userID, int propertyID, string propertyValue, out string PropertyName, out string PropertyValue, out int PropertyID)
        {
            string propertyString = "";
            int output;
            output = UserRepository.SaveProfilePropertyValue(userID, propertyID, propertyValue, out propertyString);
            PropertyName = propertyString;
            PropertyValue = propertyValue;
            PropertyID = propertyID;
            return output;
        }

        public bool SaveProfilePropertyValues(Guid userId, List<ProfilePropertyValue> properties)
        {
            foreach (ProfilePropertyValue property in properties)
            {
                if (property.IsTransient)
                {
                    string response;
                    int propertyId;
                    string propertyName;
                    string propertyValue;
                    int propertyValueId;

                    if ((propertyId = this.CreateProfileProperty(property.PropertyName, out response)).Equals(default(int)))
                    {
                        if(this.SaveProfilePropertyValue(userId, propertyId, property.PropertyValue, out propertyName, out propertyValue, out propertyValueId).Equals(default(int)))
                        {
                            throw new Exception("Property was not saved");
                        }
                    } else throw new Exception("Property was not saved");
                }
                else if (property.HasChange)
                {
                    if (!this.UpdateProfilePropertyValue(userId, property.PropertyID, property.PropertyValue))
                    {
                        throw new Exception("Property was not saved");
                    }
                }
            }
            // assumed if we make it to this point an exception was not thrown and the result is true.
            return true;
        }

        public bool UpdateProfilePropertyValue(Guid userID, int propertyID, string propertyValue)
        {
            return UserRepository.UpdateProfilePropertyValue(userID, propertyID, propertyValue);
        }

        public void DeleteProfilePropertyValue(int profilePropertyValueID)
        {
            UserRepository.DeleteProfilePropertyValue(profilePropertyValueID);
        }
        
        #endregion

        public bool InsertChangeRecords(Guid sId, Guid cId, IList<Common.Domain.PropertyChange> changes)
        {
            throw new NotImplementedException();
        }


        public bool InsertChangeRecords(Guid bindingId, Guid sId, Guid cId, IList<Common.Domain.PropertyChange> changes)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Guid, IList<Common.Domain.PropertyChange>> GetChangeRecordsByChangeContact(int id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Guid, IList<Common.Domain.PropertyChange>> GetChangeRecordsByChangeContact(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid GetProfilePropertyUserGuid_ByValue(string propertyValue, string propertyName)
        {
            return UserRepository.GetProfilePropertyUserGuid_ByValue(propertyValue, propertyName);
        }
        public Guid GetProfilePropertyUserGuid_ByValue(string propertyValue, int propertyId)
        {
            return UserRepository.GetProfilePropertyUserGuid_ByValue(propertyValue, propertyId);
        }
        public Guid GetProfilePropertyUserGuid_ByValue(string propertyValue, string propertyName, int propertyId)
        {
            return UserRepository.GetProfilePropertyUserGuid_ByValue(propertyValue, propertyName, propertyId);
        }
    }
}
