using System;

namespace Sims.CheatEngine.Domains
{
    public class UrlBuilder : UriBuilder
    {
        public Uri BaseUri { get; }

        public UrlBuilder(string baseUri)
            : this(new Uri(baseUri))
        {
            
        }

        public Uri GetUri(string relativeUrl) => new Uri(BaseUri, relativeUrl);

        public UrlBuilder(Uri baseUri)
            : base(baseUri)
        {
            BaseUri = baseUri;
        }
    }
}