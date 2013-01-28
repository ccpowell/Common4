using System;
using System.Collections.Generic;
using DRCOG.Common.Security;

namespace DRCOG.Common.Interfaces
{
    /// <summary>
    /// Service responsible for querying the domain for informaiton about users.
    /// </summary>
    public interface IGenericDomainSearchService
    {
        bool ValidateUser(string userIdentity, string password);
        
        
        /// <summary>
        /// Gets the email address of a user given an identity.
        /// </summary>
        /// <param name="userIdentity">The identity of the user. Can be the UserPrincipalName or SamAccountName.</param>
        /// <returns>The email address</returns>
        String GetEmailAddress(String userIdentity);

        /// <summary>
        /// Gets properties of a user from Active Directory.
        /// </summary>
        /// <param name="userIdentity">The identity of the user. Can be the UserPrincipalName or SamAccountName.</param>
        /// <param name="propertiesToLoad">The properties to retrieve from Active Directory</param>
        /// <returns>A Dictionary containing all properties and values for a user.</returns>
        IDictionary<ActiveDirectoryAttribute, String> GetUser(String userIdentity,
            IEnumerable<ActiveDirectoryAttribute> propertiesToLoad);
    }
}
