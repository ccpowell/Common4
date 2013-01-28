using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using DRCOG.Common.DesignByContract;
using DRCOG.Common.Security;
using DRCOG.Common.Interfaces;
using DRCOG.Common.Interfaces.ActiveDirectorySupport;
using DRCOG.Common.Util;

namespace DRCOG.Common.Services
{
    public class GenericDomainSearchService : IGenericDomainSearchService
    {
        public IDomainSearchConfig Configuration { get; private set; }
        public ISearchFilterFactory FilterFactory { get; private set; }

        public GenericDomainSearchService(IDomainSearchConfig config, ISearchFilterFactory filterFactory)
        {
            Check.Require(config != null, "A configuration must be provided.");
            Check.Require(filterFactory != null, "A filter factory must be provided.");
            Configuration = config;
            FilterFactory = filterFactory;
        }

        protected virtual DirectoryEntry GetRoot()
        {
            if (Configuration.HasCredentials)
            {
                return new DirectoryEntry(Configuration.Path, Configuration.UserName, Configuration.Password, 
                    AuthenticationTypes.Secure);
            }
            return new DirectoryEntry(Configuration.Path);
        }

        #region IDomainSearchService Members

        public bool ValidateUser(string userIdentity, string password)
        {
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "cognet.drcog.org", Configuration.UserName, Configuration.Password))
                {
                    try { var server = context.ConnectedServer; }
                    catch { return false; }
                    if (context.ValidateCredentials(userIdentity, password))
                    {
                        return true;
                    }
                }
            }
            catch(Exception exc)
            {
                string stop = "test";
            }
            return false;
        }


        public virtual String GetEmailAddress(string userIdentity)
        {
            String email;
            var user = GetUser(userIdentity, new[] { ActiveDirectoryAttribute.Mail });
            user.TryGetValue(ActiveDirectoryAttribute.Mail, out email);
            return email ?? String.Empty;
        }

        public virtual IDictionary<ActiveDirectoryAttribute, String> GetUser(string userIdentity)
        {
            Check.Require(!String.IsNullOrEmpty(userIdentity),
                "Cannot search the domain with a null or empty userIdentity");
            var values = new Dictionary<ActiveDirectoryAttribute, String>();

            List<ActiveDirectoryAttribute> attributes = EnumHelper.EnumToList<ActiveDirectoryAttribute>();
            var user = GetUser(userIdentity, attributes.ToArray());
            
            return values;
        }

        public virtual IDictionary<ActiveDirectoryAttribute, String> GetUser(string userIdentity,
            IEnumerable<ActiveDirectoryAttribute> propertiesToLoad)
        {
            Check.Require(!String.IsNullOrEmpty(userIdentity),
                "Cannot search the domain with a null or empty userIdentity");
            var values = new Dictionary<ActiveDirectoryAttribute, String>();

            using (var root = GetRoot())
            {

                using (var searcher = new DirectorySearcher(root))
                {
                    searcher.Filter = FilterFactory.GetUserFilter(userIdentity);
                    //(&(objectclass=user)(userprincipalname=dtucker))
                    //(&(objectclass=user)(objectcategory=person)(userprincipalname=dtucker))
                    foreach (ActiveDirectoryAttribute attribute in propertiesToLoad)
                    {
                        searcher.PropertiesToLoad.Add(attribute.ToString());
                    }
                    var result = searcher.FindOne();

                    if (result != null)
                    {
                        foreach (var attribute in propertiesToLoad)
                        {
                            var attributeString = attribute.ToString();
                            if (result.Properties[attributeString] != null &&
                                result.Properties[attributeString].Count > 0)
                            {
                                values.Add(attribute, result.Properties[attributeString][0].ToString());
                            }
                            else
                            {
                                values.Add(attribute, "");
                            }
                        }
                    }
                }
            }
            return values;
        }

        #endregion
    }
}
