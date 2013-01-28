using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using DRCOG.Common.Domain.Search;

namespace DRCOG.Common.Service.MemberShipServiceSupport.Domain.Search
{
    public interface ISearchSpecification : ISpecification<SearchCriteria>
    {
        ISearchSpecification WithApplicationName(string name);
        ISearchSpecification WithApplicationGuid(Guid guid);
        ISearchSpecification WithSystemName(string name);
        ISearchSpecification WithSystemID(int id);
        ISearchSpecification WithFirstName(string name);
        ISearchSpecification WithLastName(string name);
        ISearchSpecification WithEmailAddress(string name);
        ISearchSpecification WithPhoneNumber(string name);
    }
}
