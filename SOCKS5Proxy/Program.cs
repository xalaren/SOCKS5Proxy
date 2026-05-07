using System.Net;
using Microsoft.Extensions.Logging;
using VpnHood.Core.Proxies.Socks5ProxyServers;

namespace SOCKS5Proxy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(b => b.AddSimpleConsole(o => o.TimestampFormat = "HH:mm:ss "));
            var configuration = new ConfigurationModel(2080);
            var server = Server.GetInstance(configuration, loggerFactory);

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            server.Run(cts.Token);
            
        }
    }
}
