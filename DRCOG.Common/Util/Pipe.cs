using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Util
{
    public static class test
    {
        public static TResult Pipe<T, TResult>(this T obj, Func<T, TResult> f)
        {
            return f(obj);
        }
    }
}
