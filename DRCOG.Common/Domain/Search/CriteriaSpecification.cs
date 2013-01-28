using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DRCOG.Common.Domain.Search
{
    [DataContract]
    public abstract class CriteriaSpecification<TEntity> : 
        ICriteriaSpecification<TEntity> where TEntity : class
    {
        [DataMember]
        public IDictionary<string, object> Criteria { get; private set; }
        [DataMember]
        public IDictionary<string, object> Property { get; private set; }

        public CriteriaSpecification()
        {
            Criteria = new Dictionary<string, object>();
            Property = new Dictionary<string, object>();
        }
    }
}
