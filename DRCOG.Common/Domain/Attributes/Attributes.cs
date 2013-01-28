using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Domain.Attributes
{
    /// <summary>
    /// Facilitates indicating which property(s) describe the unique signature of an 
    /// entity.  See Entity.GetTypeSpecificSignatureProperties() for when this is leveraged.
    /// </summary>
    /// <remarks>
    /// This is intended for use with <see cref="Entity{IdT}" />.
    /// </remarks>
    [Serializable]
    public class DomainSignatureAttribute : Attribute { }

    [Serializable]
    public class IgnoreOnUpdateAttribute : Attribute { }

    [Serializable]
    public class SortableAttribute : Attribute
    {
        public String SubProperty;
    }

    [Serializable]
    public class TableAttribute : Attribute
    {
        public String Name;
    }

    [Serializable]
    public class ColumnAttribute : Attribute
    {
        public String Name;
    }
}
