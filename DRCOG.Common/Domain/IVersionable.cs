using System;

namespace DRCOG.Common.Domain
{
    /// <summary>
    /// Indicates if the entity is versionable in a data store
    /// </summary>
    public interface IVersionable<VType>
    {
        /// <summary>
        /// Indicates the current version of the object.
        /// </summary>
        VType Version { get; set; }

        String VersionToString();
    }
}
