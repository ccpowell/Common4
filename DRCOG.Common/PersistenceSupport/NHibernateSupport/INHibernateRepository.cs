using System;
using System.Collections.Generic;
using DRCOG.Common.Domain;
using System.Linq.Expressions;
using DRCOG.Common.Collections;

namespace DRCOG.Common.PersistenceSupport.NHibernateSupport
{
    public interface INHibernateRepository<T, IdT> : ILinqEnabledRepository<T, IdT> where T : IEntity<IdT>
    {
        /// <summary>
        /// This is a performance saver for data access.  Creates a proxy of <typeparamref name="T"/>
        /// with the specified id. This call will not result in a query being run, but if the object is not 
        /// found when the object is accessed, then a exception will be thrown.
        /// see http://nhforge.org/blogs/nhibernate/archive/2009/04/30/nhibernate-the-difference-between-get-load-and-querying-by-id.aspx
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <returns>A proxy of <typeparamref name="T"/>.</returns>
        T Load(IdT id);

        /// <summary>
        /// This is a performance saver for data access.  Creates a proxy of <typeparamref name="T"/>
        /// with the specified id. This call will not result in a query being run, but if the object is not 
        /// found when the object is accessed, then a exception will be thrown.
        /// see http://nhforge.org/blogs/nhibernate/archive/2009/04/30/nhibernate-the-difference-between-get-load-and-querying-by-id.aspx
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <returns>A proxy of <typeparamref name="T"/>.</returns>
        T Load(T entity);

        /// <summary>
        /// Uses the futures capability of NHibernate to batch query
        /// </summary>
        /// <typeparam name="PType">The type of the value to be used for the expression comparison.</typeparam>
        /// <param name="propertyExpression">The property expression to use for the comparison</param>
        /// <param name="value">the value to be used for the expression copmairson</param>
        /// <returns></returns>
        IEnumerable<T> GetAll<PType>(Expression<Func<T, PType>> propertyExpression, PType value);
        /// <summary>
        /// Uses the futures capability of NHibernate to batch query
        /// </summary>
        /// <typeparam name="PType">The type of the value to be used for the expression comparison.</typeparam>
        /// <param name="expressions">A key value pair collection of the expressions and the values to use for comparisons</param>
        /// <returns></returns>
        IEnumerable<T> GetAll<PType>(IEnumerable<KeyValuePair<Expression<Func<T, PType>>, PType>> expressions);
    }
}
