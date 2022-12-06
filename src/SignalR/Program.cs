using ConsoleApp.Models;
using In66.Chat.Api.Models.Entities.Business.Chat;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SqlSugar;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

Console.WriteLine("Hello, World!");
SqlSugarClient db;

//const string ServerUrl = "http://localhost:5100";
//const string ServerUrl = "http://121.43.34.54:8020";
const string ServerUrl = "http://imapitest.66in.net";


string username, password; 
List<OnlineUsersForChat> clientusers = new();

await Main();

 async Task Main()
{
    //数据库连接
    db = new SqlSugarClient(new ConnectionConfig()
    {
        ConnectionString = "Data Source=localhost;port=3306;User ID=root;Password=root;Database=chat;CharSet=utf8mb4;sslmode=none;AllowLoadLocalInfile=true;",
        DbType = DbType.MySql,
        IsAutoCloseConnection = true,
        InitKeyType = InitKeyType.Attribute
    });

    username = "yanh";

    password = "yanh";

    await Task.Run(() => RunConnection(HttpTransportType.WebSockets, username, password));
}

/// <summary>
/// RunConnection
/// </summary>
/// <param name="transportType"></param>
/// <returns></returns>
async Task RunConnection(HttpTransportType transportType, string username, string password)
{

    //连接地址
    var url = $"{ServerUrl}/chatHub";
    // 参数
    var hubConnection = new HubConnectionBuilder()
        .WithUrl(url, options =>
        {
            options.Transports = transportType;
            options.AccessTokenProvider = () => GetJwtToken(username, password);
        })
        .WithAutomaticReconnect()
        .Build();

    // Broadcast
    hubConnection.On<string, string>("Broadcast", (sender, message) =>
    {
        Console.WriteLine($"[{username}]\n{sender}:{message}");
    });

    //onlineChatUser
    hubConnection.On<string>("onlineChatUser", (message) => {
        Console.WriteLine($"onlineChatUser");

        clientusers = JsonConvert.DeserializeObject<List<OnlineUsersForChat>>(message);

        foreach (var item in clientusers)
        {
            Console.WriteLine($"在线用户连接Id: [{item.ConnectionId}]\n在线用户: [{item.Name}]\n");
        }
    });

    //ReceiveMessage
    hubConnection.On<string,string,string>("ReceiveMessage", (sender, receiver, message) =>
    {
        Console.WriteLine($"{sender}向{receiver}发送消息{message}");
    });

    // 建立连接
    await hubConnection.StartAsync();
    Console.WriteLine($"[{username}] Connection Started...");

    //广播
    await Broadcast(hubConnection, username);

    while (true)
    {
        string method = "";

        Console.WriteLine("选择方法:0.退出 1.发送消息");

        method = Console.ReadLine();

        if (method.Trim().ToLower() == "0")
        {
            break;
        }

        switch (method)
        {
            case "1":
                var chatUserOne = clientusers.Where(q => q.Name ==username).FirstOrDefault();

                var chatUserTwo=clientusers.Where(q => q.Name != username).FirstOrDefault();

                Console.WriteLine("请输入发送的消息:");
                //消息
                var message = Console.ReadLine();
                await SendChat(hubConnection, chatUserOne.ConnectionId, chatUserTwo.ConnectionId, chatUserOne.Name, chatUserTwo.Name, $"{chatUserOne.Name}向{chatUserTwo.Name}:========{message}======");
                break;
            default:
                Console.WriteLine("输入有误");
                break;
        }
    }

    Console.ReadKey(true);
}

/// <summary>
/// GetJwtToken
/// </summary>
/// <param name="username"></param>
/// <param name="pwd"></param>
/// <returns></returns>
async Task<string> GetJwtToken(string username, string pwd)
{
    //密码md5
    var Pwd = NETCore.Encrypt.EncryptProvider.Md5(pwd);

    var user = db.Queryable<ChatUser>().First(it => it.ChatUserName == username && it.Password == Pwd);

    var claims = new Claim[]
    {
            new Claim(JwtRegisteredClaimNames.Jti,user.ChatUserId.ToString()),
            new Claim(ClaimTypes.NameIdentifier,user.ChatUserId.ToString()),
            new Claim(ClaimTypes.Role,user.ChatUserName),
            new Claim("source","219"),
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
async Task Broadcast(HubConnection hubConnection, string username)
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
async Task SendChat(HubConnection hubConnection, string selfConnectionId, string connectId, string sender, string receiver, string message)
{
    await hubConnection.SendAsync("SendChat", selfConnectionId, connectId, sender, receiver, message);
}




