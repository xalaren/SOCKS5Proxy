namespace SOCKS5Proxy.Console.RunConfiguration
{
    public class RunConfig
    {
        public RunCommands RunCommand { get; set; }
        public List<string>? PropertiesRequest { get; set; }
        public Dictionary<string, string>? PropertiesConfig { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
