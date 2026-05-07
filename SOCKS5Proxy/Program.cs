using Microsoft.Extensions.Logging;
using SOCKS5Proxy;
using SOCKS5Proxy.Configurations;

ILoggerFactory loggerFactory = LoggerFactory.Create
(
    builder => builder.AddConsole()
);
var configuration = new ConfigurationFileOrDefaultFactory(loggerFactory).Create();
using var server = Server.GetInstance(configuration, loggerFactory);

var programLogger = loggerFactory.CreateLogger<Program>();
programLogger.LogInformation("Press \"Ctrl + C\" to stop server");
var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

server.Run(cts.Token);
programLogger.LogInformation("Press any key to close console...");

Console.ReadKey();