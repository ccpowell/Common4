using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Services.MemberShipServiceSupport
{
    public class ContextWrapper<S, T>
    {
        public S Session { get; set; }
        public T Entity { get; set; }
    }
}
