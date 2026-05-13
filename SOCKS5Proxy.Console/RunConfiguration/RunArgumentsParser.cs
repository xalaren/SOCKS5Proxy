using System.Text.RegularExpressions;
using SOCKS5Proxy.Server.Configurations;

namespace SOCKS5Proxy.Console.RunConfiguration
{
    public static class RunArgumentsParser
    {
        public static RunConfig Parse(string[] args)
        {
            var runConfig = new RunConfig()
            {
                RunCommand = RunCommands.Default
            };

            if (args == null || args.Length == 0) return runConfig;

            try
            {
                runConfig.RunCommand = ParseRunCommand(args[0]);

                switch(runConfig.RunCommand)
                {
                    case RunCommands.Help:
                        break;
                    case RunCommands.Echo:
                        runConfig.PropertiesRequest = ParseEchoPropertiesRequest(args);
                        break;
                    case RunCommands.Config:
                        runConfig.PropertiesConfig = ParseConfigProperties(args);
                        break;
                }
            }
            catch(Exception ex)
            {
                runConfig.ErrorMessage = ex.Message;
            }

            return runConfig;
        }

        private static RunCommands ParseRunCommand(string arg)
        {
            switch (arg)
            {
                case "/config":
                    return RunCommands.Config;
                case "/echo":
                    return RunCommands.Echo;
                case "/help":
                    return RunCommands.Help;
                default:
                    throw new ArgumentException(RunMessages.InvalidArgument);
            }
        }

        private static Dictionary<string, string> ParseConfigProperties(string[] args)
        {
            CheckArgs(args);

            Regex regex = new Regex(@"(\w+)\s*=\s*(\d+.\d+.\d+.\d|\d+|(\w|\s*)+)(\n|\s|$)?");
            Dictionary<string, string> properties = new Dictionary<string, string>();
            for (int i = 1; i < args.Length; i++)
            {
                if (!regex.IsMatch(args[i]))
                {
                    throw new ArgumentException(RunMessages.InvalidArgument);
                }

                string key = regex.Replace(args[i], "$1");
                string value = regex.Replace(args[i], "$2");

                properties.Add(key, value);
            }

            return properties;
        }

        private static List<string> ParseEchoPropertiesRequest(string[] args)
        {
            CheckArgs(args);

            List<string> propertiesRequest = new List<string>();
            for(int i = 1; i < args.Length; i++)
            {
                if (string.Equals(args[i], "ip", StringComparison.OrdinalIgnoreCase))
                {
                    propertiesRequest.Add(nameof(Configuration.IP));
                }
                else if (string.Equals(args[i], "port", StringComparison.OrdinalIgnoreCase))
                {
                    propertiesRequest.Add(nameof(Configuration.Port));
                }
                else if (string.Equals(args[i], "username", StringComparison.OrdinalIgnoreCase))
                {
                    propertiesRequest.Add(nameof(Configuration.Username));
                }
                else throw new ArgumentException(RunMessages.UnknownProperty);
            }

            return propertiesRequest;
        }

        private static void CheckArgs(string[] args)
        {
            if (args.Length < 2) throw new ArgumentException(RunMessages.RequireArgument);
        }
    }
}
