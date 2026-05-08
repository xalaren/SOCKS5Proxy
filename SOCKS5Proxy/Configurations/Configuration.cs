namespace SOCKS5Proxy.Configurations
{
    public record Configuration(string IP, int Port, string? Username = null, string? Password = null);
}
