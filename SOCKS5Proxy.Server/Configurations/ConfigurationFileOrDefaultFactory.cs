using Microsoft.Extensions.Logging;
using SOCKS5Proxy.Server.Configurations.Abstractions;

namespace SOCKS5Proxy.Server.Configurations
{
    public class ConfigurationFileOrDefaultFactory : ConfigurationFactory
    {
        private const string LocalhostIP = "127.0.0.1";
        private const int Socks5ProxyDefaultPort = 1080;
        private ILoggerFactory loggerFactory;

        public ConfigurationFileOrDefaultFactory(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }
        public override Configuration Create()
        {
            var configurationReader = new ConfigurationReader(loggerFactory);
            return configurationReader.Read() ?? new Configuration(LocalhostIP, Socks5ProxyDefaultPort);
        }
    }
}
