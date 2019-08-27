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

        public string Request(string relativeUrl, IDictionary<string, string> queryString = null)
        {
            return _webClient.DownloadString(queryString == null 
                ? _apiUrlBuilder.GetUri(relativeUrl)
                : _apiUrlBuilder.GetUriWithQueryString(relativeUrl, queryString)
                );
        }

        public IEnumerable<T> RequestAsArray<T>(string relativeUrl, IDictionary<string, string> queryString = null)
        {
            var request = Request(relativeUrl, queryString);
            if (request.StartsWith("["))
                return JArray.Parse(request).ToObject<IEnumerable<T>>();

            throw new NotSupportedException();
        }

        public T RequestAsObject<T>(string relativeUrl, IDictionary<string, string> queryString = null)
        {
            var request = Request(relativeUrl, queryString);
            if (request.StartsWith("{"))
                return JToken.Parse(request).ToObject<T>();

            throw new NotSupportedException();
        }
    }
}