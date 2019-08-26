using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Sims.CheatEngine.Domains
{
    public class DataAccess
    {
        private readonly UrlBuilder _apiUrlBuilder;
        private readonly WebClient _webClient;

        public DataAccess(string apiUrl)
        {
            _apiUrlBuilder = new UrlBuilder(apiUrl);
            _webClient = new WebClient { BaseAddress = apiUrl };
        }

        public string Request(string relativeUrl)
        {
            return _webClient.DownloadString(_apiUrlBuilder.GetUri(relativeUrl));
        }

        public IEnumerable<T> RequestAsArray<T>(string relativeUrl)
        {
            var request = Request(relativeUrl);
            if (request.StartsWith("["))
                return JArray.Parse(request).ToObject<IEnumerable<T>>();

            throw new NotSupportedException();
        }

        public T RequestAsObject<T>(string relativeUrl)
        {
            var request = Request(relativeUrl);
            if (request.StartsWith("{"))
                return JToken.Parse(request).ToObject<T>();

            throw new NotSupportedException();
        }
    }
}