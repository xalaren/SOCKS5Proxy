using Microsoft.Extensions.Logging;
namespace SOCKS5Proxy.Server.Configurations
{
    public class ConfigurationFileFactory : ConfigurationDefaultFactory
    {
        protected readonly ILoggerFactory LoggerFactory;

        public ConfigurationFileFactory(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
        }
        public override Configuration Create()
        {
            var configurationReader = new ConfigurationReader(LoggerFactory);
            Configuration? configuration = configurationReader.Read();

            if(configuration == null)
            {
                return base.Create();
            }

            return configuration;
        }
    }
}
