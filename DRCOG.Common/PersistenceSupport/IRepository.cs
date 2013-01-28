using System;
using System.Collections.Generic;
using DRCOG.Common.Domain;
using DRCOG.Common.Collections;

namespace DRCOG.Common.PersistenceSupport
{
    /// <summary>
    /// Interface using the repository pattern for data access.  
    /// Standard data access mechanisms should use this interface for this application.
    /// </summary>
    /// <typeparam name="T">The type of object the repository will hold</typeparam>
    /// <typeparam name="IdT">The data type of the unique identifier for the object within the data store.</typeparam>
    public interface IRepository<T, IdT> where T : IEntity<IdT>
    {
        /// <summary>
        /// Saves an new object to the store.
        /// </summary>
        /// <param name="entity">The object to save</param>
        void Save(T entity);

        /// <summary>
        /// Retrieves an object from the data store.
        /// </summary>
        /// <param name="id">The unique identifier of the object.</param>
        /// <returns>The object if found.</returns>
        T Get(IdT id);

        /// <summary>
        /// Retrieves all <typeparamref name="T"/> from the data store.
        /// </summary>
        /// <returns>A <see cref="IList{T}"/> of the objects.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Updates an object in the data store.
        /// </summary>
        /// <param name="updated">The object to update containing the new values.</param>
        void Update(T updated);

        /// <summary>
        /// Deletes an object from the data store.
        /// </summary>
        /// <param name="entity">The object to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Returns a paged collection of objects.
        /// </summary>
        /// <param name="firstResult">The first result in the database to start with.</param>
        /// <param name="pageSize">The number of records to return</param>
        /// <returns></returns>
        IPagedCollection<T> GetAllPaged(Int32 firstResult, Int32 pageSize);

        /// <summary>
        /// Checks if the entity has a unique signature that does not conflict with other entities in the data store.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <returns>true if the entity is unique</returns>
        Boolean IsUnique(T entity);
    }
}
