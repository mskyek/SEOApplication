using SEOApplication.Application.Proxies.External;
using SEOApplication.Application.SEO;
using SEOApplication.Domain;
using SEOApplication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEOApplication.Infrastructure.Proxies.External
{
    public class SEOControllerProxy : ISEOControllerProxy
    {
        private static HttpClient _client;
        private Serilog.ILogger _logger => Serilog.Log.ForContext<SEOControllerProxy>();

        public SEOControllerProxy(HttpClient client)
        {
            _client = client;
        }
        public Task<SEOResponse> GetSearchResults(string searchTerm, string resultNumber, SearchEngineType searchEngine, string searchURL)
        {
            var searchResults = string.Empty;
            List<string> result = new List<string>();
            var formattedSearchTerm = searchTerm.Replace(' ', '+');
            string url = string.Format("https://www.{0}/search?num={1}&q={2}", EnumMapper.From(searchEngine), resultNumber, formattedSearchTerm);

            try
            {
                using (HttpResponseMessage response = _client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        searchResults = content.ReadAsStringAsync().Result;
                    }
                }
                if (searchResults != null)
                {
                    var formattedSearchURL = FormatSearchURL(searchURL);
                    result = AdvancedScraper(searchResults, formattedSearchURL, searchEngine);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong getting search results.");
                throw new ArgumentException();
            }

            return Task.FromResult(new SEOResponse { Result = result });
        }

        private string FormatSearchURL(string searchURL)
        {
            var validURL = searchURL;
            if (!searchURL.Substring(0, 4).Equals("www."))
            {
                validURL = string.Format("www.{0}", searchURL);
            }

            return validURL;
        }

        private List<string> AdvancedScraper(string searchResults, string searchURL, SearchEngineType searchEngine)
        {
            List<string> searchPositions = new List<string>();
            var parseTerm = GetParsingValue(searchEngine);
            string[] values = searchResults.Split(new string[] { parseTerm }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < values.Length; i ++)
            {
                foreach (Match m in Regex.Matches(values[i], @"" + searchURL + ""))
                {
                    searchPositions.Add(i.ToString());
                    break;
                }
            }

            return searchPositions;
        }

        private string GetParsingValue(SearchEngineType searchEngineType)
        {
            var parsedValue = string.Empty;
            switch (searchEngineType)
            {
                case SearchEngineType.Google:
                    parsedValue = "div class=\"ZINbbc xpd O9g5cc uUPGi\"";
                    break;
                case SearchEngineType.Bing:
                    parsedValue = "li class=\"b_algo\"";
                    break;
            }

            return parsedValue;
        }
    }
}
