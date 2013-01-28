using System;
using System.Security.Principal;

namespace DRCOG.Common.Security
{
    public interface IGenericUserPrincipal<TIdentity> : IPrincipal
    {
        TIdentity UserIdentity { get; }
    }
}
