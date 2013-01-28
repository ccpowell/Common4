using DRCOG.Common.Interfaces.ActiveDirectorySupport;
using DRCOG.Common.DesignByContract;

namespace DRCOG.Common.Services.ActiveDirectorySupport
{
    public class DefaultSearchFilterFactory : ISearchFilterFactory
    {
        public const string USER_FILTER_STRING =
            "(&(objectclass=user)(objectcategory=person)({0}))";
            //"(&(objectCategory=person)({0}))";

        public string GetUserFilter(string userIdentity)
        {
            Check.Require(!string.IsNullOrEmpty(userIdentity), "Cannot build a filter from a null or empty userIdentity");
            // ReSharper disable PossibleNullReferenceException
            string filter = userIdentity.Contains(@"\")
                                ? "samaccountname=" + userIdentity.Substring(userIdentity.IndexOf(@"\") + 1)
                                : "samaccountname=" + userIdentity;//: "userprincipalname=" + userIdentity;
            // ReSharper restore PossibleNullReferenceException
            return string.Format(USER_FILTER_STRING, filter);
        }
    }
}
