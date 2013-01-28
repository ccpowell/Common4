using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DRCOG.Common.Domain
{
    public interface IEntity
    {

    }

    /// <summary>
    /// Interface for an entity that can be saved and retrieved froma data store.
    /// </summary>
    /// <typeparam name="IdT">The data type of the unique identifier</typeparam>
    public interface IEntity<IdT> : IEntity
    {
        /// <summary>
        /// The data store unique identity of the object.
        /// </summary>
        IdT EntityId { get; set; }

        /// <summary>
        /// Transient objects are not associated with an item already in storage.  For instance,
        /// a Customer is transient if its Id is 0.  It's virtual to allow NHibernate-backed 
        /// objects to be lazily loaded.
        /// </summary>
        Boolean IsTransient();

        /// <summary>
        /// Gets the properties which make up 
        /// the "domain signature" of the object. 
        /// Signature properties are not necessary for entities using natural keys.
        /// </summary>
        /// <returns></returns>
        IEnumerable<PropertyInfo> GetSignatureProperties();

        String GetEntityTable();
    }
}
