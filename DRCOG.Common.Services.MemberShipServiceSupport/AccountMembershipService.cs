using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using DRCOG.Common.Service.MemberShipServiceSupport.Interfaces;
using DRCOG.Common.DesignByContract;
using DRCOG.Common.Services.MemberShipServiceSupport.Domain;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? MembershipProviderFactory.GetProvider(MembershipProviderType.DRCOG);
        }

        public AccountMembershipService(MembershipProviderType providerName)
        {
            _provider = MembershipProviderFactory.GetProvider(MembershipProviderType.DRCOG);
        }


        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }


        public Guid PersonGuid
        {
            get
            {
                MembershipUser user = Membership.GetUser();
                if (user != null)
                {
                    return (Guid)user.ProviderUserKey;
                }
                else { return Guid.Empty; }
                
            }
        }

        public virtual bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            var value = _provider.ValidateUser(userName, password);

            return value;
        }

        public virtual void UpdateUser(Guid guid)
        {
            MembershipUser user = _provider.GetUser(guid, false);
            user.IsApproved = false;
            user.LastActivityDate = DateTime.UtcNow;
            _provider.UpdateUser(user);
        }
        public virtual void UpdateUser(Profile profile)
        {
            MembershipUser user = _provider.GetUser(profile.EntityId, false);
            user.IsApproved = profile.IsApproved;
            user.Comment = profile.Comment;
            user.Email = profile.RecoveryEmail;
            user.LastActivityDate = DateTime.UtcNow;
            _provider.UpdateUser(user);
        }

        public virtual void UpdateUser(string userName, string email, string question, string answer)
        {
            new NotImplementedException();
        }

        public virtual string GetUserName(Guid guid)
        {
            MembershipUser user = _provider.GetUser(guid, false);

            
            return user.UserName;
        }

        public virtual string GetUserNameByEmail(string email)
        {
            return _provider.GetUserNameByEmail(email);
        }

        public virtual MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public virtual MembershipCreateStatus CreateUser(string userName, string password, string email, string question, string answer)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");
            if (String.IsNullOrEmpty(question)) throw new ArgumentException("Value cannot be null or empty.", "question");
            if (String.IsNullOrEmpty(answer)) throw new ArgumentException("Value cannot be null or empty.", "answer");

            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, question, answer, true, null, out status);
            return status;
        }

        public virtual bool DeleteUser(string username, bool deleteAllRelatedData)
        {

            return _provider.DeleteUser(username, deleteAllRelatedData);
        }

        public virtual bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

        public virtual PasswordResetResult ResetPassword(string userName)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");

            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                _provider.UnlockUser(userName);
                Check.Require(!_provider.RequiresQuestionAndAnswer, "This method is only available when the provider Question and Answer is not required");

                string password = currentUser.ResetPassword();
                currentUser.IsApproved = true;
                Membership.UpdateUser(currentUser);
                _provider.UnlockUser(userName);

                return new PasswordResetResult(PasswordResetResultType.Successful, password);
            }
            catch (ArgumentException)
            {
                return new PasswordResetResult(PasswordResetResultType.Error);
            }
            catch (MembershipPasswordException exc)
            {
                return new PasswordResetResult(PasswordResetResultType.Error);
            }
        }

        public virtual bool ResetPassword(string userName, string newPassword)
        {
            return ResetPassword(userName, newPassword, String.Empty);
        }

        public virtual bool ResetPassword(string userName, string newPassword, string answer)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(answer) && _provider.RequiresQuestionAndAnswer) throw new ArgumentException("Value cannot be null or empty.", "answer");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                string oldPassword;

                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                _provider.UnlockUser(userName);
                if (!_provider.RequiresQuestionAndAnswer)
                {
                    
                    oldPassword = currentUser.ResetPassword();
                }
                else
                {
                    oldPassword = currentUser.ResetPassword(answer);
                }
                if (currentUser.ChangePassword(oldPassword, newPassword))
                {
                    currentUser.IsApproved = true;
                    Membership.UpdateUser(currentUser);
                    _provider.UnlockUser(userName);
                    return true;
                }
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException exc)
            {
                return false;
            }
        }

        public virtual int GetNumberOfUsersOnline()
        {
            return _provider.GetNumberOfUsersOnline();
        }
    }
}
