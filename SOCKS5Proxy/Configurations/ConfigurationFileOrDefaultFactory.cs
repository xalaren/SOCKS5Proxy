using Microsoft.Extensions.Logging;
using SOCKS5Proxy.Configurations.Abstractions;

namespace SOCKS5Proxy.Configurations
{
    internal class ConfigurationFileOrDefaultFactory : ConfigurationFactory
    {
        private const int Socks5ProxyDefaultPort = 1080;
        private ILoggerFactory loggerFactory;

        public ConfigurationFileOrDefaultFactory(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }
        public override Configuration Create()
        {
            var configurationReader = new ConfigurationReader(loggerFactory);
            return configurationReader.Read() ?? new Configuration(Socks5ProxyDefaultPort);
        }
    }
}
