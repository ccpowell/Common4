using System;
using System.Security.Principal;

namespace DRCOG.Common.Security
{
    public interface IGenericUserIdentity : IIdentity
    {
        Boolean IsStandardAccount { get; }

        String ActiveDirectoryName { get; }

        String FirstName { get; }

        String LastName { get; }

        String FullName { get; }
    }

    public interface IGenericUserIdentity<IdType> : IGenericUserIdentity
    {
        IdType Id { get; }
    }
}
