using Microsoft.Extensions.Logging;
namespace SOCKS5Proxy.Server.Configurations
{
    public class ConfigurationPropertiesFactory : ConfigurationFileFactory
    {
        private readonly Dictionary<string, string>? properties;
        public ConfigurationPropertiesFactory(ILoggerFactory loggerFactory, Dictionary<string, string>? properties) : base(loggerFactory)
        {
            this.properties = properties;
        }
        public override Configuration Create()
        {
            var logger = LoggerFactory.CreateLogger<ConfigurationPropertiesFactory>();
            if (properties == null || properties.Count == 0) return base.Create();

            string ip = ConfigurationDefaults.IP;
            int port = ConfigurationDefaults.Port;
            string? username = null;
            string? password = null;

            foreach (var key in properties.Keys)
            {
                switch (key.ToLower())
                {
                    case "ip":
                        ip = properties[key];
                        break;
                    case "port":
                        port = int.Parse(properties[key]);
                        break;
                    case "username":
                        username = properties[key];
                        break;
                    case "password":
                        password = properties[key];
                        break;
                    default:
                        throw new ArgumentException("Invalid arguments provided!");
                }
            }
            
            return new Configuration(ip, port, username, password);
        }
    }
}
