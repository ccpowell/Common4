using System;
using System.Linq;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace DRCOG.Common.PersistenceSupport.LinqSupport
{
    /// <summary>
    /// Interface for Entity Framework <see cref="ObjectContext"/>s to implement
    /// </summary>
    public interface IObjectContext : IDisposable
    {
        /// <summary>
        /// Creates an <see cref="System.Linq.IQueryable{T}"/> in the current object context
        /// by using the specified query string.
        /// </summary>
        /// <typeparam name="T">The type of entity for the query</typeparam>
        /// <param name="queryString">The query string to be executed.</param>
        /// <returns>An <see cref="System.Data.Objects.ObjectQuery{T}"/> of the specified type.</returns>
        IQueryable<T> CreateQuery<T>(string queryString);
        /// <summary>
        /// Adds an object to the object context.
        /// </summary>
        /// <param name="entitySetName">Represents the entity set name, which may optionally be qualified by the
        /// entity container name.</param>
        /// <param name="entity">The object to add.</param>
        void AddObject(String entitySetName, Object entity);
        /// <summary>
        /// Persists all updates to the store and resets change tracking in the object
        /// context.
        /// </summary>
        /// <returns>The number of objects in an System.Data.Objects.EntityState.Added, System.Data.Objects.EntityState.Modified,
        /// or System.Data.Objects.EntityState.Deleted state when System.Data.Objects.ObjectContext.SaveChanges()
        /// was called.
        /// </returns>
        Int32 SaveChanges();
        /// <summary>
        /// Applies property changes from a detached object to an object already attached
        /// to the object context.
        /// </summary>
        /// <param name="entitySetName">The name of the entity set to which the object belongs.</param>
        /// <param name="changed">The detached object that has property updates to apply to the original object.</param>
        void ApplyPropertyChanges(String entitySetName, Object changed);
        /// <summary>
        /// Marks an object for deletion.
        /// </summary>
        /// <param name="entity">An object that specifies the entity to delete. The object can be in any state
        /// except System.Data.Objects.EntityState.Detached.</param>
        /// <exception cref="ArgumentNullException">
        /// The entity is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The entity does not exist.
        /// </exception>
        void DeleteObject(Object entity);
        /// <summary>
        /// Attaches an object or object graph to the object context when the object
        /// has an entity key.
        /// </summary>
        /// <param name="entity">The object to attach.</param>
        void Attach(IEntityWithKey entity);
        /// <summary>
        /// Removes the object from the object context.
        /// </summary>
        /// <param name="entity">
        /// Object to be detached. Only the entity is removed; if there are any related
        /// objects that are being tracked by the same System.Data.Objects.ObjectStateManager,
        /// those will not be detached automatically.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The entity is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The entity is not associated with this System.Data.Objects.ObjectContext
        /// (for example, was newly created and not associated with any context yet,
        /// or was obtained through some other context, or was already detached).
        /// </exception>
        void Detach(object entity);
        /// <summary>
        /// Gets the object state manager used by the object context to track object
        /// changes.
        /// </summary>
        ObjectStateManager ObjectStateManager { get; }
    }
}
