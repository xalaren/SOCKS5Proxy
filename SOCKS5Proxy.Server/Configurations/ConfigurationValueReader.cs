using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SOCKS5Proxy.Server.Configurations
{
    public class ConfigurationValueReader
    {
        private readonly ILoggerFactory loggerFactory;

        public ConfigurationValueReader(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public List<object> ReadValues(Configuration configuration, List<string>? properties)
        {
            List<object> propertiesValues = new List<object>();
            if (properties == null || properties.Count == 0) return propertiesValues;

            var logger = loggerFactory.CreateLogger<ConfigurationValueReader>();
            try
            {
                foreach (var property in properties)
                {
                    object? propertyValue = configuration.GetType().GetProperty(property)?.GetValue(configuration);
                    if (propertyValue == null) continue;

                    propertiesValues.Add(propertyValue);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString() + "\n" + ex.StackTrace);
            }

            return propertiesValues;
        }
    }
}
