using System.Net;
using Microsoft.Extensions.Logging;
using VpnHood.Core.Proxies.Socks5ProxyServers;

namespace SOCKS5Proxy
{
    internal class Server
    {
        private readonly ILogger<Socks5ProxyServer> logger;
        private Socks5ProxyServer socks5ProxyServer = null!;
        private static Server? instance;
        private static object locker = new object();
        protected Server(ConfigurationModel configuration, ILoggerFactory loggerFactory) 
        {
            logger = loggerFactory.CreateLogger<Socks5ProxyServer>();

            logger.LogInformation("Initializing server...");
            var options = new Socks5ProxyServerOptions
            {
                ListenEndPoint = new IPEndPoint(IPAddress.Loopback, configuration.Port),
                Username = configuration.Username,
                Password = configuration.Password
            };
            socks5ProxyServer = new Socks5ProxyServer(options);
        }

        public void Run(CancellationToken cancellationToken)
        {
            try
            {
               if(socks5ProxyServer.IsStarted)
                {
                    throw new InvalidOperationException("Server is already running!");
                }

                socks5ProxyServer.Start();
                logger.LogInformation("Server started...");

                while(!cancellationToken.IsCancellationRequested) { }

                socks5ProxyServer.Stop();
                logger.LogInformation("Server stopped...");
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public static Server GetInstance(ConfigurationModel configuration, ILoggerFactory loggerFactory)
        {
            if(instance == null)
            {
                lock(locker)
                {
                    return new Server(configuration, loggerFactory);
                }
            }

            return instance;
        }
    }
}
