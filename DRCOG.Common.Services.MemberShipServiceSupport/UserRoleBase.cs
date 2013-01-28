using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using DRCOG.Common.Domain;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Web.Security;
using DRCOG.Common.Domain.Attributes;
using DRCOG.Common.Util;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    public interface IUserRoleBase
    {
        //bool IsInRole(string role);
        //void Load();
        //void Save();
    }

    [DataContract]
    public partial class UserRoleBase : NotifiableEntity<Guid>
    {
        private int _personId;
        //private string _comment;
        protected bool _hidePropertyNotFoundText;

        [DataMember]
        public List<ProfilePropertyValue> ProfileProperties { get; set; }

        public UserRoleBase()
        {
            Roles = new Dictionary<string, Dictionary<string, bool>>();
        }

        [IgnoreOnUpdate]
        [DataMember]
        public int PersonID
        {
            get { return _personId; }
            set
            {
                NotifyProperyChange("Profile.PersonID", _personId, value);
                _personId = value;
            }
        }

        //
        // Summary:
        //     Gets the user identifier from the membership data source for the user.
        //
        // Returns:
        //     The user identifier from the membership data source for the user.
        [IgnoreOnUpdate]
        [DataMember]
        public Guid PersonGUID 
        {
            get { return base.EntityId; }
            set { base.EntityId = value; }
        }

        [IgnoreOnUpdate]
        [DataMember]
        public String PersonShortGuid
        {
            get { return ShortGuid.Encode(base.EntityId); }
            set { base.EntityId = ShortGuid.Decode(value); }
        }

        [DataMember]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DataMember]
        [DisplayName("Security Question")]
        public string Question { get; set; }

        [DataMember]
        [DisplayName("Security Answer")]
        public string Answer { get; set; }

        

        // Summary:
        //     Gets or sets application-specific information for the membership user.
        //
        // Returns:
        //     Application-specific information for the membership user.
        [DataMember]
        [UIHint("Multiline")]
        public virtual string Comment
        {
            get { return GetValue("Comment"); }
            set { SetValue("Comment", value); }

            //get { return _comment; }
            //set
            //{
            //    NotifyProperyChange("Profile.Comment", _comment, value);
            //    _comment = value;
            //}
        }

        public void SetValue(string propertyName, string value)
        {
            ProfilePropertyValue property;
            String oldValue = String.Empty;
            if (ProfileProperties == null)
                ProfileProperties = new List<ProfilePropertyValue>();

            var index = ProfileProperties.FindIndex(x => x.PropertyName == propertyName);
            if (index >= 0)
            {
                property = ProfileProperties[index];
                oldValue = property.PropertyValue;
                if (oldValue == value) return;
                property.HasChange = true;
                property.IsUnused = false;
                property.PropertyValue = value;

                ProfileProperties[index] = property;
            }
            else
            {
                property = new ProfilePropertyValue()
                {
                    IsUnused = false
                    ,
                    PropertyName = propertyName
                    ,
                    PropertyValue = value
                    ,
                    IsTransient = true
                };
                ProfileProperties.Add(property);
            }

            NotifyProperyChange("Profile.ProfileProperty." + property.PropertyName, oldValue, value);
        }

        public string GetValue(string propertyName)
        {
            if (!ProfileProperties.Any(x => x.PropertyName == propertyName))
            {
                if (this._hidePropertyNotFoundText)
                    return String.Empty;
                return CustomProfilePropertyValueResultType.PropertyNotFound.ToString();
            }

            var value = ProfileProperties.FirstOrDefault(x => x.PropertyName == propertyName);

            return value != null ? value.PropertyValue : String.Empty;
        }
        
        //
        // Summary:
        //     Gets the date and time when the user was added to the membership data store.
        //
        // Returns:
        //     The date and time when the user was added to the membership data store.
        [DataMember]
        public virtual DateTime CreationDate { get; set; }
        
        //
        // Summary:
        //     Gets or sets whether the membership user can be authenticated.
        //
        // Returns:
        //     true if the user can be authenticated; otherwise, false.
        [DataMember]
        public virtual bool IsApproved { get; set; }
        //
        // Summary:
        //     Gets a value indicating whether the membership user is locked out and unable
        //     to be validated.
        //
        // Returns:
        //     true if the membership user is locked out and unable to be validated; otherwise,
        //     false.
        [DataMember]
        public virtual bool IsLockedOut { get; set; }
        //
        // Summary:
        //     Gets or sets the date and time when the membership user was last authenticated
        //     or accessed the application.
        //
        // Returns:
        //     The date and time when the membership user was last authenticated or accessed
        //     the application.
        [DataMember]
        public virtual DateTime LastActivityDate { get; set; }
        //
        // Summary:
        //     Gets the most recent date and time that the membership user was locked out.
        //
        // Returns:
        //     A System.DateTime object that represents the most recent date and time that
        //     the membership user was locked out.
        [DataMember]
        public virtual DateTime LastLockoutDate { get; set; }
        //
        // Summary:
        //     Gets or sets the date and time when the user was last authenticated.
        //
        // Returns:
        //     The date and time when the user was last authenticated.
        [DataMember]
        public virtual DateTime LastLoginDate { get; set; }
        //
        // Summary:
        //     Gets the date and time when the membership user's password was last updated.
        //
        // Returns:
        //     The date and time when the membership user's password was last updated.
        [DataMember]
        public virtual DateTime LastPasswordChangedDate { get; set; }

        [IgnoreOnUpdate]
        [DataMember]
        public Dictionary<string, Dictionary<string,bool>> Roles { get; set; }

        [DataMember]
        public string[] RolesUnused { get; set; }

        /// <summary>
        /// Verify if a user is in a specific role. Super user option enabled if role Administrator exists.
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="role"></param>
        /// <returns>true or false</returns>
        public virtual bool IsInRole(string providerName, string role)
        {
            if (this.Roles != null)
            {
                var collection = this.Roles.Where(x => x.Key == providerName)
                    .Select(x => x.Value)
                    .Select(x => x.Where(y => (y.Value == true && y.Key == role) || (y.Key == "Administrator" && y.Value == true)))
                    .FirstOrDefault();


                var value = collection != null ? (collection.Count() > 0 ? true : false) : false;
                return value;
                //foreach (string s in this.Roles)
                //{
                //    if (s.ToLower().Equals(role.ToLower()))
                //    {
                //        return true;
                //    }
                //}
            }
            return false;
        }

        //public virtual bool IsInRole(string role)
        //{
        //    if (this.Roles != null)
        //    {
        //        foreach (string s in this.Roles)
        //        {
        //            if (s.ToLower().Equals(role.ToLower()))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        //public void LoadRoles(IPrincipal user)
        //{
        //    this.Roles = ((RolePrincipal)user).GetRoles();
        //}

    }
}
