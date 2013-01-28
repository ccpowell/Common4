using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DRCOG.Common
{
    public static class EnvironmentVariableHelper
    {
        /// <summary>
        /// Helper class for substituting environment variables.
        /// 
        /// </summary>
        public static readonly Regex _regex = new Regex(@"%(\w+)%");

        /// <summary>
        /// Resolves the environment variables found in a string.
        /// </summary>
        /// <param name="text">The text to scan.</param>
        /// <see cref="http://www.forkcan.com/viewcode/264/Utility-class-for-retrieving-environment-variables"/>
        /// <example>
        /// <configuration>
        ///  <publishSettings path="\\%devVM%\sites\" />
        ///</configuration>
        /// string example = "%myvariable%";
        /// string result = EnvironmentVariableHelper.ResolveVariables(example);
        /// Debug.WriteLine(result);
        /// </example>
        public static string ResolveVariables(string text)
        {
            return _regex.Replace(text,
                          match =>
                          {
                              string envname = match.Groups[1].Value;
                              string value = Environment.GetEnvironmentVariable(envname);
                              return value ?? match.Value;
                          });
        }
    }
}
