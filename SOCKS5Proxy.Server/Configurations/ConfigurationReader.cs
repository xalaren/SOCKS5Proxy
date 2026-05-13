using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SOCKS5Proxy.Server.Configurations
{
    public class ConfigurationReader
    {
        private const string FileName = "ProxyConfig.json";
        private readonly ILogger<ConfigurationReader> logger;

        public ConfigurationReader(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<ConfigurationReader>();
        }

        public Configuration? Read()
        {
            try
            {
                if (!File.Exists(FileName)) return null;

                using(var reader = new StreamReader(FileName, Encoding.UTF8))
                {
                    var rawJson = reader.ReadToEnd();
                    var configuration = JsonConvert.DeserializeObject<Configuration>(rawJson);

                    return configuration;
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString() + "\n" + ex.StackTrace);
                return null;
            }
        }
    }
}
