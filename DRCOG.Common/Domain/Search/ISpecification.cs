using System.ServiceModel;

namespace DRCOG.Common.Domain.Search
{
    public interface ISpecification<TEntity> where TEntity : class
    {
        //bool IsSatisfiedBy(TEntity t);
    }
}
