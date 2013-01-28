using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using DRCOG.Common.Domain.Search;

namespace DRCOG.Common.Service.MemberShipServiceSupport.Domain.Search
{
    public class CriteriaSearchSpecification : CriteriaSpecification<SearchCriteria>, ISearchSpecification
    {

        public CriteriaSearchSpecification()
        {
        }

        public CriteriaSearchSpecification(CriteriaSearchSpecificationSettings specSettings)
        {
            this.WithApplicationName(specSettings.ApplicationName);
            this.WithApplicationGuid(specSettings.ApplicationGuid);
            this.WithSystemName(specSettings.SystemName);
            this.WithSystemID(specSettings.SystemID);
            this.WithFirstName(specSettings.FirstName);
            this.WithLastName(specSettings.LastName);
            this.WithEmailAddress(specSettings.EmailAddress);
            this.WithPhoneNumber(specSettings.PhoneNumber);
        }

        public ISearchSpecification WithApplicationName(string name)
        {
            this.Criteria.Add("@ApplicationName", name);
            return this;
        }

        public ISearchSpecification WithApplicationGuid(Guid guid)
        {
            this.Criteria.Add("@ApplicationGuid", guid);
            return this;
        }

        public ISearchSpecification WithSystemName(string name)
        {
            this.Criteria.Add("@SystemName", name);
            return this;
        }

        public ISearchSpecification WithSystemID(int ID)
        {
            this.Criteria.Add("@SystemID", ID);
            return this;
        }

        public ISearchSpecification WithFirstName(string name)
        {
            this.Criteria.Add("@FirstName", name);
            return this;
        }

        public ISearchSpecification WithLastName(string name)
        {
            this.Criteria.Add("@LastName", name);
            return this;
        }

        public ISearchSpecification WithEmailAddress(string name)
        {
            this.Criteria.Add("@EmailAddress", name);
            return this;
        }

        public ISearchSpecification WithPhoneNumber(string name)
        {
            this.Criteria.Add("@PhoneNumber", name);
            return this;
        }
    }
}
