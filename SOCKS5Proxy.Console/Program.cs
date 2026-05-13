using Microsoft.Extensions.Logging;
using SOCKS5Proxy.Console.RunConfiguration;
using SOCKS5Proxy.Server;
using SOCKS5Proxy.Server.Configurations;

ILoggerFactory loggerFactory = LoggerFactory.Create
(
    builder => builder.AddConsole()
);

var runConfig = RunArgumentsParser.Parse(args);

if(!string.IsNullOrWhiteSpace(runConfig.ErrorMessage))
{
    Console.WriteLine(runConfig.ErrorMessage);
    return;
}

if (runConfig.RunCommand == RunCommands.Help)
{
    Help();
    return;
}

if (runConfig.RunCommand == RunCommands.Echo)
{
    Echo(runConfig, loggerFactory);
    return;
}

Run(runConfig, loggerFactory);


static void Help()
{
    Console.WriteLine(RunMessages.Help);
}

static void Echo(RunConfig runConfig, ILoggerFactory loggerFactory)
{
    Configuration tempConfiguration = new ConfigurationFileFactory(loggerFactory).Create();

    var propertiesValues = new ConfigurationValueReader(loggerFactory).ReadValues(tempConfiguration, runConfig.PropertiesRequest);
    foreach (var propertyValue in propertiesValues)
    {
        Console.WriteLine(propertyValue);
    }
}

static void Run(RunConfig runConfig, ILoggerFactory loggerFactory)
{
    var programLogger = loggerFactory.CreateLogger<Program>();

    try
    {
        Configuration configuration = new ConfigurationPropertiesFactory(loggerFactory, runConfig.PropertiesConfig)
            .Create();

        using var server = Runner.GetInstance(configuration, loggerFactory);

        programLogger.LogInformation("Press \"Ctrl + C\" to stop server");
        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };
        server.Run(cts.Token);

        Console.WriteLine("Press any key to close console");
        Console.ReadKey();
    }
    catch (Exception ex)
    {
        programLogger.LogError(ex.Message + "\n" + ex.StackTrace);
    }
}