using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using DRCOG.Common.Services.MemberShipServiceSupport.Domain;

namespace DRCOG.Common.Service.MemberShipServiceSupport.Interfaces
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }
        Guid PersonGuid { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email, string question, string answer);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool DeleteUser(string username, bool deleteAllRelatedData);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        PasswordResetResult ResetPassword(string userName);
        bool ResetPassword(string userName, string newPassword);
        bool ResetPassword(string userName, string newPassword, string answer);
        void UpdateUser(Guid guid);
        string GetUserName(Guid guid);
        string GetUserNameByEmail(string email);

        int GetNumberOfUsersOnline();
    }
}
