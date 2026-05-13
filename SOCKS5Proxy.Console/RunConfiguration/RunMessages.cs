namespace SOCKS5Proxy.Console.RunConfiguration
{
    public sealed class RunMessages
    {
        public const string Help =
            "Program run template: SOCKS5Proxy.Console.{exe | dll} [command] [properties]\n\n" +
            "Commands:\n\n" +
            "\t/echo {field1} [field2] [field3] ... - Print requested configuration field\n\n" +
            "\t\tExamples: \n\n" +
            "\t\tSOCKS5Proxy.Console.exe /echo ip\n" +
            "\t\tSOCKS5Proxy.Console.exe /echo port\n" +
            "\t\tSOCKS5Proxy.Console.exe /echo username\n" +
            "\t\tSOCKS5Proxy.Console.exe /echo ip port\n" +
            "\t\tSOCKS5Proxy.Console.exe /echo ip port username\n" +
            "\t\t...\n\n" +
            "\t/config {{field1}={value1}} [{field2}={value2}] ... - Create configuration from command line\n\n" +
            "\t\tExamples: \n\n" +
            "\t\tSOCKS5Proxy.Console.exe /config port=1080\n" +
            "\t\tSOCKS5Proxy.Console.exe /config ip=\"127.0.0.1\" port=1080\n" +
            "\t\tSOCKS5Proxy.Console.exe /config ip=\"127.0.0.1\" port=1080 username=\"alessa\" password=\"qwerty123\n" +
            "\t\t...\n\n";

        public const string RequireArgument = "Using command required at least one argument! Use /help to view commands.";
        public const string InvalidArgument = "Invalid arguments provided. Use /help to view commands";
        public const string UnknownProperty = "Unknown property requested!";

    }
}
