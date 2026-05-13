# SOCKS5Proxy
<img width="128" height="128" alt="socks5proxyicon" src="https://github.com/user-attachments/assets/00ca5a96-d9a9-4f03-9076-2bba43de18ba" />

`SOCKS5Proxy` is a mini portable console application to start your SOCKS5 proxy server. 

## Note 
Project using a VpnHood.Core.Proxies library under the hood.

## Requirements
Installed .NET 10 Runtime.

## Usage

**1. Run application with command line and arguments.**

  For windows: `SOCKS5Proxy.exe /config ip="127.0.0.1" port=1080`
  
  For Linux: `dotnet SOCKS5Proxy.dll /config ip="127.0.0.1" port=1080`
  
  List of available arguments:
  * `ip` - What IP address will the proxy server be accessible at (by default is `localhost`);
  * `port` - Connection port (by default is `1080`);
  * `username` - Connection username (optional property);
  * `password` - Connection password (optional property);

**2. Create a `ProxyConfig.json` file at application root folder, then enter properties.**

  List of properties:
  * `IP` - What IP address will the proxy server be accessible at (by default is `localhost`);
  * `Port` - Connection port (by default is `1080`);
  * `Username` - Connection username (optional property);
  * `Password` - Connection password (optional property);

  Example:
  ```json
  {
    "IP": "127.0.0.1",
    "Port": 1080,
    "Username": "alessa",
    "Password": "qwerty123"
  }
  ```

**3. Get configuration parameters.**

  For windows: `SOCKS5Proxy.exe /echo ip port`
  
  For Linux: `dotnet SOCKS5Proxy.dll /echo ip port`

  Result: 
  
  `127.0.0.1`
  
  `1080`

  List of available parameters:
  * `ip`
  * `port`
  * `username`
