namespace SOCKS5Proxy.Configurations
{
    public record Configuration(int Port, string? Username = null, string? Password = null);
}
