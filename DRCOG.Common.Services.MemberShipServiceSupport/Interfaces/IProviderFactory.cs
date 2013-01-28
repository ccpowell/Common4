using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration.Provider;

namespace DRCOG.Common.Services.MemberShipServiceSupport.Interfaces
{
    public interface IProviderFactory<T, E> where T : ProviderBase where E : struct, IConvertible
    {
        T GetProvider(E provider);
    }
}
