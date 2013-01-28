using DRCOG.Common.DesignByContract.Exceptions;

namespace DRCOG.Common.Interfaces.ActiveDirectorySupport
{
    public interface ISearchFilterFactory
    {
        /// <summary>
        /// Builds a filter that can be used with when querying Active Directory for user information.
        /// </summary>
        /// <param name="userIdentity">The identity of the user.</param>
        /// <exception cref="DesignByContractException">Thrown if the userIdentity is invalid</exception>
        /// <returns></returns>
        string GetUserFilter(string userIdentity);
    }
}
