using SOCKS5Proxy.Server.Configurations.Abstractions;

namespace SOCKS5Proxy.Server.Configurations
{
    public class ConfigurationDefaultFactory : ConfigurationFactory
    {
        public override Configuration Create()
        {
            return new Configuration(ConfigurationDefaults.IP, ConfigurationDefaults.Port);
        }
    }
}
