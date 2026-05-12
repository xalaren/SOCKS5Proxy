using SOCKS5Proxy.Server.Configurations;

namespace SOCKS5Proxy.Console
{
    public static class ConsoleArgsParser
    {
        private static readonly string[] InfoAliases = ["-info", "-i"];
        private static readonly string[] LineAliases = ["-line", "-l"];
        public static ConsoleArgs? Parse(string[] args)
        {
            if (args == null || args.Length == 0) return null;

            ActionArgs actions = ActionArgs.None;
            List<string> infoParameters = new List<string>();
            var isInfo = InfoAliases.Contains(args[1]);

            if(isInfo)
            {
                actions = ActionArgs.Info;

                var loweredArgs = args.Select(arg => arg.ToLower()).ToArray();
                if(loweredArgs.Contains("ip"))
                {
                    infoParameters.Add(nameof(Configuration.IP));
                }

                if (loweredArgs.Contains("port"))
                {
                    infoParameters.Add(nameof(Configuration.Port));
                }

                if (loweredArgs.Contains("username"))
                {
                    infoParameters.Add(nameof(Configuration.Port));
                }
            }
            else if(!isInfo && LineAliases.Contains(args[1]))
            {
                actions = ActionArgs.Line;

                
            }

            foreach (var infoAlias in InfoAliases)
            {
                if (args[1] == infoAlias)
                {
                    actions = ActionArgs.Info;
                    break;
                }
            }


            return new ConsoleArgs()
            {
                Actions = actions
            };
        }
    }
}
