namespace DRCOG.Common.Security
{
    public enum ActiveDirectoryAttribute
    {
        /// <summary>
        /// This property is useful for logging in a large forest Active Directory architecutre. 
        /// This is also a unique property throughout the forest. 
        /// This property is often abbreviated as UPN.
        /// Should map to the user's email address 
        /// </summary>
        UserPrincipalName,
        /// <summary>
        /// Telephone number of the user.
        /// </summary>
        TelephoneNumber,
        /// <summary>
        /// Email address of the user.
        /// </summary>
        Mail,
        /// <summary>
        /// Name of the user. Should be in the format "LAST, FIRST"
        /// </summary>
        DisplayName,
        /// <summary>
        /// State or Province
        /// </summary>
        St,
        /// <summary>
        /// User's department
        /// </summary>
        Department,
        /// <summary>
        /// User's first name
        /// </summary>
        GivenName,
        /// <summary>
        /// Description of the user
        /// </summary>
        Description,
        /// <summary>
        /// Last time the user's account was changed.
        /// </summary>
        WhenChanged,
        /// <summary>
        /// The user's home directory on the network.
        /// </summary>
        HomeDirectory,
        /// <summary>
        /// When the user's account was created.
        /// </summary>
        WhenCreated,
        /// <summary>
        /// The home drive letter of the user.
        /// </summary>
        HomeDrive,
        /// <summary>
        /// Contains the CN, all OUs, and DCs for a user
        /// </summary>
        DistinguishedName,
        /// <summary>
        /// The user's company
        /// </summary>
        Company,
        /// <summary>
        /// The user's mail nickname. Should have the format "FIRST.LAST"
        /// </summary>
        MailNickName,
        /// <summary>
        /// The user's last name.
        /// </summary>
        Sn,
        /// <summary>
        /// The user's common name. This LDAP attribute is made up from givenName joined to SN.
        /// </summary>
        Cn,
        /// <summary>
        /// The user's title.
        /// </summary>
        Title,
        /// <summary>
        /// This is an old NT 4.0 logon ID. This value must be unique in the domain.
        /// </summary>
        SamAccountName,
        /// <summary>
        /// The user's cell phone number.
        /// </summary>
        Mobile,
        /// <summary>
        /// The user's location.
        /// </summary>
        L,
        /// <summary>
        /// The user's country.
        /// </summary>
        C
    }
}