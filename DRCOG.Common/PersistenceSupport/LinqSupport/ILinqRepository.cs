using System;
using DRCOG.Common.Domain;

namespace DRCOG.Common.PersistenceSupport.LinqSupport
{
    public interface ILinqRepository<T, IdT> : ILinqEnabledRepository<T, IdT> where T : IEntity<IdT>
    {
        /// <summary>
        /// The name of the entity set to which <typeparamref name="T"/> belongs in the <see cref="Context"/>.
        /// If it is not assigned by a sub class, this class assumes the naming convention by Linq was used: {[ShortClassName]Set}
        /// </summary>
        String EntitySetName { get; }
    }
}
