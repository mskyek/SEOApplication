using SEOApplication.Domain.Enums;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEOApplication.Domain
{
    public static class EnumMapper
    {
        private static ILogger logger = Log.Logger;

        private static readonly Dictionary<Enum, string> data = new Dictionary<Enum, string>()
        {
            { SearchEngineType.Google, "google.com" },
            { SearchEngineType.Bing, "bing.com" }
        };

        public static T To<T>(string dataVal) where T : struct
        {
            var value = data.FirstOrDefault(d => typeof(T) == d.Key.GetType());
            var matches = data.Where<KeyValuePair<Enum, string>>(p => typeof(T) == p.Key.GetType() && p.Value.ToUpper().Equals(dataVal.Trim().ToUpper()));

            if (!matches.Any())
            {
                logger.Warning("Enum value not found. Enum Type: {@type}. Data value: {dataVal}", typeof(T).Name, dataVal);
            }

            return (T)Enum.Parse(typeof(T),
                     (matches.DefaultIfEmpty(value).FirstOrDefault<KeyValuePair<Enum, string>>().Key.ToString()));
        }

        public static string From<T>(T key) where T : struct
        {
            return data
                     .Where<KeyValuePair<Enum, string>>(p => typeof(T) == p.Key.GetType() && (key).Equals(p.Key))
                     .First<KeyValuePair<Enum, string>>().Value.ToString();
        }
    }
}
