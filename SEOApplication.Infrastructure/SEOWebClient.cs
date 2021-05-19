using System;
using System.Net;

namespace SEOApplication.Infrastructure
{
    public class SEOWebClient : WebClient
    {
        public int Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest webRequest = base.GetWebRequest(uri);
            webRequest.Timeout = Timeout * 1000;
            return webRequest;
        }
    }
}
