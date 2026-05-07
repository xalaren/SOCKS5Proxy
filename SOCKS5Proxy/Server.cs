using System.Net;
using Microsoft.Extensions.Logging;
using VpnHood.Core.Proxies.Socks5ProxyServers;
using SOCKS5Proxy.Configurations;

namespace SOCKS5Proxy
{
    internal class Server : IDisposable
    {
        private readonly ILogger<Socks5ProxyServer> logger;
        private Socks5ProxyServer socks5ProxyServer = null!;
        private static Server? instance;
        private static object locker = new object();
        protected Server(Configuration configuration, ILoggerFactory loggerFactory) 
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
                logger.LogInformation($"Server started on {socks5ProxyServer.ListenerEndPoint}...");

                while(!cancellationToken.IsCancellationRequested) { }

                socks5ProxyServer.Stop();
                logger.LogInformation("Server stopped...");
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public static Server GetInstance(Configuration configuration, ILoggerFactory loggerFactory)
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

        public void Dispose()
        {
            socks5ProxyServer.Stop();
            socks5ProxyServer.Dispose();
        }
    }
}
