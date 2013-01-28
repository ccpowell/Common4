using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRCOG.Common.Collections;
using DRCOG.Common.Domain;
using System.Linq.Expressions;

namespace DRCOG.Common.PersistenceSupport
{
    public interface ILinqEnabledRepository<T, IdT> : IRepository<T, IdT> where T : IEntity<IdT>
    {
        /// <summary>
        /// Get a single <typeparamref name="T"/> based on an expression
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns></returns>
        T Get(Expression<Func<T, Boolean>> expression);

        /// <summary>
        /// Gets all records from the database meeting the requirements of the expression.
        /// </summary>
        /// <param name="expression">The expression used to evaluate.</param>
        /// <returns></returns>
        IEnumerable<T> GetAll(Expression<Func<T, Boolean>> expression);

        /// <summary>
        /// Gets all records from the database meeting the requirements of each expression.
        /// </summary>
        /// <param name="expressions">The expressions used to evaluate.</param>
        /// <returns></returns>
        IEnumerable<T> GetAll(IEnumerable<Expression<Func<T, Boolean>>> expressions);

        /// <summary>
        /// Sorts and then pages objects into a collection
        /// </summary>
        /// <param name="firstResult">The first result wiithin the page.</param>
        /// <param name="pageSize">The number of records to return</param>
        /// <param name="criteria">The criteria by which to filter</param>
        /// <returns></returns>
        IPagedCollection<T> GetAllPaged(Int32 firstResult, Int32 pageSize,
            Expression<Func<T, Boolean>> criteria);

        /// <summary>
        /// Sorts and then pages objects into a collection
        /// </summary>
        /// <param name="firstResult">The first result wiithin the page.</param>
        /// <param name="pageSize">The number of records to return</param>
        /// <param name="criteria">The criteria by which to filter</param>
        /// <returns></returns>
        IPagedCollection<T> GetAllPaged(Int32 firstResult, Int32 pageSize,
            IEnumerable<Expression<Func<T, Boolean>>> criteria);

        /// <summary>
        /// Sorts and then pages objects into a collection
        /// </summary>
        /// <param name="firstResult">The first result wiithin the page.</param>
        /// <param name="pageSize">The number of records to return</param>
        /// <param name="property">The property by which to sort represented by a Linq expression</param>
        /// <param name="direction">The direction of the sort</param>
        /// <returns></returns>
        IPagedCollection<T> GetAllPaged(Int32 firstResult, Int32 pageSize,
            Expression<Func<T, Object>> sortingProperty, Sorting.Direction direction);

        /// <summary>
        /// Sorts and then pages objects into a collection
        /// </summary>
        /// <param name="firstResult">The first result wiithin the page.</param>
        /// <param name="pageSize">The number of records to return</param>
        /// <param name="expressions">Criteria used to search with</param>
        /// <param name="sortingProperty">The property by which to sort represented by a Linq expression</param>
        /// <param name="direction">The direction of the sort</param>
        /// <returns></returns>
        IPagedCollection<T> GetAllPaged(Int32 firstResult, Int32 pageSize,
            IEnumerable<Expression<Func<T, Boolean>>> expressions,
            Expression<Func<T, Object>> sortingProperty, Sorting.Direction direction);

        /// <summary>
        /// Sorts and then pages objects into a collection
        /// </summary>
        /// <param name="firstResult">The first result wiithin the page.</param>
        /// <param name="pageSize">The number of records to return</param>
        /// <param name="expression">Criteria used to search with</param>
        /// <param name="sortingProperty">The property by which to sort represented by a Linq expression</param>
        /// <param name="direction">The direction of the sort</param>
        /// <returns></returns>
        IPagedCollection<T> GetAllPaged(Int32 firstResult, Int32 pageSize,
            Expression<Func<T, Boolean>> expression,
            Expression<Func<T, Object>> sortingProperty, Sorting.Direction direction);

        /// <summary>
        /// Sorts and then pages objects into a collection
        /// </summary>
        /// <param name="property">The property by which to sort represented by a Linq expression</param>
        /// <param name="direction">The direction of the sort</param>
        /// <param name="pageNumber">The page of containing the results.</param>
        /// <param name="pageSize">The number of records to return</param>
        /// <returns></returns>
        IPagedCollection<T> GetAllPaged(Expression<Func<T, Object>> sortingProperty,
            Sorting.Direction direction, Int32 pageNumber, Int32 pageSize);

        /// <summary>
        /// Sorts and then pages objects into a collection
        /// </summary>
        /// <param name="expressions">Criteria used to search with</param>
        /// <param name="sortingProperty">The property by which to sort represented by a Linq expression</param>
        /// <param name="direction">The direction of the sort</param>
        /// <param name="pageNumber">The page of containing the results.</param>
        /// <param name="pageSize">The number of records to return</param>
        /// <returns></returns>
        IPagedCollection<T> GetAllPaged(IEnumerable<Expression<Func<T, Boolean>>> expressions,
            Expression<Func<T, Object>> sortingProperty, Sorting.Direction direction,
            Int32 pageNumber, Int32 pageSize);

        /// <summary>
        /// Sorts and then pages objects into a collection
        /// </summary>
        /// <param name="expression">Criteria used to search with</param>
        /// <param name="sortingProperty">The property by which to sort represented by a Linq expression</param>
        /// <param name="direction">The direction of the sort</param>
        /// <param name="pageNumber">The page of containing the results.</param>
        /// <param name="pageSize">The number of records to return</param>
        /// <returns></returns>
        IPagedCollection<T> GetAllPaged(Expression<Func<T, Boolean>> expression,
            Expression<Func<T, Object>> sortingProperty, Sorting.Direction direction,
            Int32 pageNumber, Int32 pageSize);

        /// <summary>
        /// Returns a generic queryable link to the database with which Linq expressions can be used.
        /// Implementations of this method may be incomplete.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetQueryable();
    }
}
