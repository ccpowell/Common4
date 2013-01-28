using System;

namespace DRCOG.Common.PersistenceSupport.NHibernateSupport
{
    /// <summary>
    /// This interface pulled from Billy McCafferty's S#arp Architecture.
    /// 
    /// Note that outside of CommitChanges(), you shouldn't have to invoke this object very often.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Removes the object from NHibernate's cache.
        /// </summary>
        /// <param name="obj"></param>
        void Evict(Object obj);
        /// <summary>
        /// Removes all objects from NHibernate's cache.
        /// </summary>
        void Clear();
        void CommitChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        Boolean HasActiveTransaction();
    }
}
