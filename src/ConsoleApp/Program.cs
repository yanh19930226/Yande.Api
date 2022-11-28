using ConsoleApp.Models;
using In66.Chat.Api.Models.Entities.Business.Chat;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SqlSugar;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class Program
{
    const string ServerUrl = "http://localhost:5100";

    static SqlSugarClient db;

    static string username, password;

    static  List<OnlineUsersForChat> clientusers = new();

    static async Task Main(string[] args)
    {
        //数据库连接
        db = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = "Data Source=localhost;port=3306;User ID=root;Password=root;Database=chat;CharSet=utf8mb4;sslmode=none;AllowLoadLocalInfile=true;",
            DbType = DbType.MySql,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        });

        //Console.WriteLine("请输入姓名:");
        //username = Console.ReadLine();

        //Console.WriteLine("请输入密码");
        //password = Console.ReadLine();

        username = "yanh";

        password = "yanh";

        await Task.Run(() => new Program().RunConnection(HttpTransportType.WebSockets, username, password));
    }

    /// <summary>
    /// signalR
    /// </summary>
    /// <param name="transportType"></param>
    /// <returns></returns>
    async Task RunConnection(HttpTransportType transportType,string username,string password)
    {
        // 参数
        var hubConnection = new HubConnectionBuilder()
            .WithUrl($"{ServerUrl}/chatHub", options =>
            {
                options.Transports = transportType;
                options.AccessTokenProvider = () => GetJwtToken(username, password);
            }).Build();

        // ReceiveMessage
        hubConnection.On<string, string>("ReceiveMessage", (sender, message) => {
            Console.WriteLine($"[{username}]\n{sender}:{message}");
        });

        // onlineChatUser
        hubConnection.On<List<OnlineUsersForChat>>("onlineChatUser", (message) => {
            clientusers = message;
            foreach (var item in clientusers)
            {
                Console.WriteLine($"在线用户: [{username}]\n");
            }
        });

        // Broadcast
        hubConnection.On<string, string>("Broadcast", (sender, message) => {
            Console.WriteLine($"[{username}]\n{sender}:{message}");
        });

        // 建立连接
        await hubConnection.StartAsync();
        Console.WriteLine($"[{username}] Connection Started...");

        // 向服务端发送一条消息
        //await hubConnection.SendAsync("Broadcast", username, $"Hello at {DateTime.Now.ToString()}");

        //广播
        await Broadcast(hubConnection, username);

        while (true)
        {
            var method = "";

            Console.WriteLine("选择方法:0.退出 1.向好友发送消息 2.");

            if (method.Trim().ToLower() == "0")
            {
                break;
            }
            switch (method)
            {
                case "1":
                    var chatUser = clientusers.FirstOrDefault();
                    var self = clientusers.Where(q => q.Name == username).FirstOrDefault();
                    await SendFriendsChat(hubConnection, self.ConnectionId, chatUser.ConnectionId, username, chatUser.Name, $"{username}向{chatUser.Name}发送消息");
                    break;
                case "2":
                    break;
                default:
                    Console.WriteLine("输入有误");
                    break;
            }
            Console.ReadLine();
        }

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
        //密码md5
        var Pwd= NETCore.Encrypt.EncryptProvider.Md5(pwd);

        var user= db.Queryable<ChatUser>().First(it => it.ChatUserName == username && it.Password == Pwd);

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Jti,user.ChatUserId.ToString()),
            new Claim(ClaimTypes.NameIdentifier,user.ChatUserId.ToString()),
            new Claim(ClaimTypes.Role,user.ChatUserName),
            new Claim(ClaimTypes.Name,user.ChatUserName),
            new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user))
        };

        var secretKey = "8kh2luzmp0oq9wfbdeasygj647vr531n";

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

        DateTime expires = DateTimeOffset.UtcNow.Add(TimeSpan.FromHours(10)).UtcDateTime;

        var token = new JwtSecurityToken(
                    issuer: "In66Admin",
                    audience: "In66",
                    claims: claims,
                    notBefore: DateTimeOffset.UtcNow.UtcDateTime,
                    expires: expires,
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
      
        return jwtToken;
    }

    /// <summary>
    /// Broadcast
    /// </summary>
    /// <param name="hubConnection"></param>
    /// <param name="username"></param>
    /// <returns></returns>
    async Task Broadcast(HubConnection hubConnection,string username)
    {
        // 向服务端发送一条消息
        await hubConnection.SendAsync("Broadcast", username, $"Hello at {DateTime.Now.ToString()}");
    }

    /// <summary>
    /// SendFriendsChat
    /// </summary>
    /// <param name="hubConnection"></param>
    /// <param name="selfConnectionId"></param>
    /// <param name="connectId"></param>
    /// <param name="sender"></param>
    /// <param name="receiver"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    async Task SendFriendsChat(HubConnection hubConnection, string selfConnectionId, string connectId, string sender, string receiver, string message)
    {
        await hubConnection.SendAsync("SendFriendsChat", selfConnectionId, connectId, sender, receiver, message);
    }
}