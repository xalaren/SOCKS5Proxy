namespace SOCKS5Proxy
{
    public record ConfigurationModel(int Port, string? Username = null, string? Password = null);
}
