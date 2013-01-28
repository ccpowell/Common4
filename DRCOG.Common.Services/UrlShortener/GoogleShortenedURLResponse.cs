using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Services.UrlShortener
{
    public class GoogleShortenedURLResponse
    {
        public string id { get; set; }
        public string kind { get; set; }
        public string longUrl { get; set; }
    }
}
