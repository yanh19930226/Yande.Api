using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class Program
{
    const string ServerUrl = "http://localhost:5000";
    static async Task Main(string[] args)
    {
        await Task.Run(() => new Program().RunConnection(HttpTransportType.WebSockets));
    }

    /// <summary>
    /// signalR
    /// </summary>
    /// <param name="transportType"></param>
    /// <returns></returns>
    async Task RunConnection(HttpTransportType transportType)
    {
        var userId = new Random().Next(2, 19).ToString();
        // 参数
        var hubConnection = new HubConnectionBuilder()
            .WithUrl($"{ServerUrl}/broadcast", options =>
            {
                options.Transports = transportType;
                options.AccessTokenProvider = () => GetJwtToken("","");
            }).Build();

        // 消息接收
        hubConnection.On<string, string>("Message", (sender, message) => {
            Console.WriteLine($"[{userId}]\n{sender}:{message}");
        });

        // 建立连接
        await hubConnection.StartAsync();
        Console.WriteLine($"[{userId}] Connection Started...");

        // 向服务端发送一条消息
        await hubConnection.SendAsync("Broadcast", userId, $"Hello at {DateTime.Now.ToString()}");

        Console.ReadKey(true);
    }

    /// <summary>
    /// 获取Token
    /// </summary>
    /// <param name="username"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    async Task<string> GetJwtToken(string username,string pwd)
    {
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Jti,""),
            new Claim(ClaimTypes.NameIdentifier,""),
            new Claim(ClaimTypes.Role,""),
            new Claim(ClaimTypes.Name,""),
        };

        var secretKey = "";

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

        DateTime expires = DateTimeOffset.UtcNow.Add(TimeSpan.FromHours(10)).UtcDateTime;

        var token = new JwtSecurityToken(
                    issuer: "",
                    audience: "",
                    claims: claims,
                    notBefore: DateTimeOffset.UtcNow.UtcDateTime,
                    expires: expires,
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
      
        return jwtToken;
    }
}