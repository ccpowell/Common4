using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Security;
using System.Globalization;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization;
using DRCOG.Common.Services.MemberShipServiceSupport.Domain;
using DRCOG.Common.Interfaces;
using DRCOG.Common.Domain.Attributes;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    #region Models

    [DataContract]
    public partial class Profile : UserRoleBase, IUserRoleBase, IProperty
    {
        
        
        public Profile()
        {
            ProfileProperties = new List<ProfilePropertyValue>();
        }
        public Profile(bool hidePropertyNotFoundText) : this()
        {
            _hidePropertyNotFoundText = hidePropertyNotFoundText;
        }

        public bool HidePropertyNotFoundText { get { return _hidePropertyNotFoundText; } set { _hidePropertyNotFoundText = value; } }

        

        [IgnoreOnUpdate]
        public string FullName { get { return this.FirstName + " " + this.LastName; } }

        [DataMember]
        [Required(ErrorMessage = "Required")]
        [DisplayName("First Name")]
        public string FirstName
        {
            get { return GetValue("FirstName"); }
            set { SetValue("FirstName", value); }
        }

        [DataMember]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Last Name")]
        public string LastName
        {
            get { return GetValue("LastName"); }
            set { SetValue("LastName", value); }
        }

        [DataMember]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Primary Contact Number")]
        public string Phone
        {
            get { return GetValue("PrimaryContact"); }
            set { SetValue("PrimaryContact", value); }
        }

        private string _username;
        private string _recoveryEmail;

        [DataMember]
        [DisplayName("UserName")]
        public string UserName 
        {
            get { return _username; }
            set 
            {
                NotifyProperyChange("Profile.UserName", _username, value);
                _username = value; 
            }
        }

        [DataMember]
        [DisplayName("DRCOG Username")]
        public string DRCOGUserName
        {
            get { return GetValue("DRCOGUserName"); }
            set { SetValue("DRCOGUserName", value); }
        } 

        //
        // Summary:
        //     Gets or sets the e-mail address for the membership user.
        //
        // Returns:
        //     The e-mail address for the membership user.
        [DataMember]
        [UIHint("Email")]
        [DisplayName("Recovery Email")]
        [Required(ErrorMessage = "Required")]
        public virtual string RecoveryEmail
        {
            get { return _recoveryEmail; }
            set
            {
                NotifyProperyChange("Profile.RecoveryEmail", _recoveryEmail, value);
                _recoveryEmail = value;
            }
        }

        [DataMember]
        [UIHint("Email")]
        [DisplayName("Personal Email")]
        public virtual string HomeEmail
        {
            get { return GetValue("HomeEmailAddress"); }
            set { SetValue("HomeEmailAddress", value); }
        }

        [DataMember]
        [UIHint("Email")]
        [DisplayName("Business Email")]
        public virtual string BusinessEmail
        {
            get { return GetValue("BusinessEmailAddress"); }
            set { SetValue("BusinessEmailAddress", value); }
        }

        [DataMember]
        [UIHint("Email")]
        [DisplayName("Alternate Email")]
        public virtual string AlternateEmail
        {
            get { return GetValue("AlternateEmailAddress"); }
            set { SetValue("AlternateEmailAddress", value); }
        }

        [DataMember]
        [DisplayName("Home Address")]
        public string Address
        {
            get { return GetValue("HomeAddress"); }
            set { SetValue("HomeAddress", value); }
        }

        [DataMember]
        [DisplayName("Home City")]
        public string City
        {
            get { return GetValue("HomeCity"); }
            set { SetValue("HomeCity", value); }
        }

        [DataMember]
        [DisplayName("Home State")]
        public string State
        {
            get { return GetValue("HomeState"); }
            set { SetValue("HomeState", value); }
        }

        [DataMember]
        [DisplayName("Home Unit")]
        public string Unit
        {
            get { return GetValue("HomeUnit"); }
            set { SetValue("HomeUnit", value); }
        }

        [DataMember]
        [UIHint("ZipCode")]
        [DisplayName("Home Zipcode")]
        public string ZipCode
        {
            get { return GetValue("HomeZipCode"); }
            set { SetValue("HomeZipCode", value); }
        }

        [DataMember]
        [DisplayName("Organization")]
        public string Organization
        {
            get { return GetValue("Organization"); }
            set { SetValue("Organization", value); }
        }

        [DataMember]
        [DisplayName("Sponsor Code")]
        public string SponsorCode { get; set; }

        [IgnoreOnUpdate]
        [DataMember]
        public bool Success { get; set; }

        

        public void SaveProfile()
        {
            try
            {
                MemberProfile profile = new MemberProfile();
                profile.Initialize(RecoveryEmail, true);

                profile.FirstName = FirstName;
                profile.LastName = LastName;
                profile.HomePhoneNumber = Phone;
                profile.HomeEmailAddress = HomeEmail;
                profile.Save();

                this.Success = true;
            }
            catch
            {
                this.Success = false;
            }
        }
    }

    #endregion
}
