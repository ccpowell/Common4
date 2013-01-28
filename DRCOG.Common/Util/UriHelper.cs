using System;
using System.Net;

namespace DRCOG.Common.Util
{
    public sealed class UriHelper
    {
        /// <summary>
        /// Check to see if the given Uri is valid and accepting requests
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static bool IsUriValid(string uri)
        {
            uri = (!uri.StartsWith("http://") || !uri.StartsWith("https://") ? uri.Insert(0, "http://") : uri).ToLower();
            Uri result;

            if (Uri.TryCreate(uri, UriKind.Absolute, out result))
            {
                try
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                    using (HttpWebResponse rsp = (HttpWebResponse)req.GetResponse())
                    {
                        if (rsp.StatusCode == HttpStatusCode.OK)
                        {
                            return true;
                        }
                    }
                }
                catch (WebException)
                {

                }
            }
            return false;
        }
    }
}
