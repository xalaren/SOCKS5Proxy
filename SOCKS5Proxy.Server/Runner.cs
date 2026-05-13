using System.Net;
using Microsoft.Extensions.Logging;
using VpnHood.Core.Proxies.Socks5ProxyServers;
using SOCKS5Proxy.Server.Configurations;

namespace SOCKS5Proxy.Server
{
    public class Runner : IDisposable
    {
        private readonly ILogger<Socks5ProxyServer> logger;
        private Socks5ProxyServer socks5ProxyServer = null!;
        private static Runner? instance;
        private static object locker = new object();
        protected Runner(Configuration configuration, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<Socks5ProxyServer>();

            logger.LogInformation("Initializing server...");
            var options = new Socks5ProxyServerOptions
            {
                ListenEndPoint = new IPEndPoint(IPAddress.Parse(configuration.IP), configuration.Port),
                Username = configuration.Username,
                Password = configuration.Password
            };
            socks5ProxyServer = new Socks5ProxyServer(options);
        }

        public void Run(CancellationToken cancellationToken)
        {
            try
            {
                if (socks5ProxyServer.IsStarted)
                {
                    throw new InvalidOperationException("Server is already running!");
                }

                socks5ProxyServer.Start();
                if(!socks5ProxyServer.IsStarted)
                {
                    logger.LogError($"Server not started!");
                    return;
                }
                logger.LogInformation($"Server started on {socks5ProxyServer.ListenerEndPoint}...");

                cancellationToken.WaitHandle.WaitOne();

                socks5ProxyServer.Stop();
                logger.LogInformation("Server stopped...");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public static Runner GetInstance(Configuration configuration, ILoggerFactory loggerFactory)
        {
            if (instance == null)
            {
                lock (locker)
                {
                    return new Runner(configuration, loggerFactory);
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
