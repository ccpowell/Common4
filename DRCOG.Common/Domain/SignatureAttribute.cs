using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Domain
{
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
        public class SignatureAttribute : Attribute { }
}
