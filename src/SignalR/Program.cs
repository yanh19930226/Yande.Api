using ConsoleApp.Models;
using In66.Chat.Api.Models.Entities.Business.Chat;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SignalR.Models;
using SqlSugar;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

Console.WriteLine("Hello, World!");
SqlSugarClient db;

const string ServerUrl = "http://localhost:5100";
//const string ServerUrl = "http://imapitest.66in.net";
string username, password,url;

HubUser robot = new();
robot.ConnectionId= "robot";
robot.Name = "智能助手";
robot.UserId= "robot";


HubUser hubUser = new();
CustomerUser CustomerUser = new();
GuestUser GuestUser = new();


await Main();

async Task Main()
{
    //数据库连接
    db = new SqlSugarClient(new ConnectionConfig()
    {
        ConnectionString = "Data Source=121.43.34.54;port=3306;User ID=root;Password=66^^66;Database=66in;CharSet=utf8mb4;sslmode=none;AllowLoadLocalInfile=true;",
        DbType = DbType.MySql,
        IsAutoCloseConnection = true,
        InitKeyType = InitKeyType.Attribute
    });

    Console.WriteLine("选择角色:1.customer 2.guest 3.xiaoliang");

    var role = Console.ReadLine();

    if (role == "customer")
    {
        username = "yanh";

        password = "999";
        
        url = $"{ServerUrl}/chatHub";

        await Task.Run(() => RunConnection(HttpTransportType.WebSockets, username, password, url, role));

    }
    else if (role == "xiaoliang")
    {
        username = "xiaoliang";

        password = "12345678";

        url = $"{ServerUrl}/chatHub";

        await Task.Run(() => RunConnection(HttpTransportType.WebSockets, username, password, url, role));
    }
    else if (role == "guest")
    {
        username = "98765432114";

        password = "123456";

        url = $"{ServerUrl}/chatHub?source=217";

        await Task.Run(() => RunConnection(HttpTransportType.WebSockets, username, password, url, role));
    }
    else
    {
        Console.WriteLine("输入错误");
    }
}

/// <summary>
/// RunConnection
/// </summary>
/// <param name="transportType"></param>
/// <returns></returns>
async Task RunConnection(HttpTransportType transportType, string username, string password,string url,string role)
{

    //参数
    var hubConnection = new HubConnectionBuilder()
        .WithUrl(url, options =>
        {
            options.Transports = transportType;
            options.AccessTokenProvider = () => GetJwtToken(username, password);
        })
        .ConfigureLogging(logging => {
            logging.SetMinimumLevel(LogLevel.Information);
            logging.AddConsole();
        })
        .WithAutomaticReconnect()
        .Build();
    //Robot
    hubConnection.On<HubUser>("RobotInfo", (message) =>
    {
        robot.Status = message.Status;
        robot.ConnectionId = message.ConnectionId;
        robot.UserId = message.UserId;
        robot.LoginTime = message.LoginTime;
        robot.Name = message.Name;
        Console.WriteLine($"机器人信息: {robot.UserId}\nCurrrentUserInfo: [{robot.Name}]\n");
    });
    //CurrrentUserInfo
    hubConnection.On<HubUser>("CurrrentUserInfo", (message) =>
    {
        hubUser.Status = message.Status;
        hubUser.ConnectionId = message.ConnectionId;
        hubUser.UserId = message.UserId;
        hubUser.LoginTime = message.LoginTime;
        hubUser.Name = message.Name;
        Console.WriteLine($"当前用户连接Id: {message.UserId}\n当前用户: [{message.Name}]\n");
    });
    //CustomerUser
    hubConnection.On<HubUser>("CustomerService", (message) =>
    {
        CustomerUser.Status = message.Status;
        CustomerUser.ConnectionId = message.ConnectionId;
        CustomerUser.UserId = message.UserId;
        CustomerUser.LoginTime = message.LoginTime;
        CustomerUser.Name = message.Name;
        Console.WriteLine($"获取到客服: {CustomerUser.UserId}\n当前客服: [{CustomerUser.Name}]\n");
    });

    //ReceiveMessage
    hubConnection.On<ChatMessage>("ReceiveMessage", (message) =>
    {
        Console.WriteLine(message.ChatLogContent);
    });

    //建立连接
    await hubConnection.StartAsync();
    Console.WriteLine($"{username}Connection Started...");

    while (true)
    {
        string method = "";

        if (role=="guest")
        {
            Console.WriteLine("选择方法:0.退出1.发送消息");
            method = Console.ReadLine();

            if (method.Trim().ToLower() == "0")
            {
                break;
            }

            switch (method)
            {
                case "1":
                    Console.WriteLine("请输入发送的消息的类型:");
                    var chatmessagetypeString = Console.ReadLine();
                    int chatmessagetype = Convert.ToInt32(chatmessagetypeString);

                    if (chatmessagetype== (int)ChatMessageTypeEnum.Question|| chatmessagetype == (int)ChatMessageTypeEnum.CustomerService)
                    {
                        string message = string.Empty;
                        if (chatmessagetype == (int)ChatMessageTypeEnum.Question)
                        {
                            Console.WriteLine("向机器人提问");
                            message = "向机器人提问";
                        }

                        if (chatmessagetype == (int)ChatMessageTypeEnum.CustomerService)
                        {
                            Console.WriteLine("向机器人获取客服:");
                            message = "向机器人获取客服";
                        }

                        await InvokeService(hubConnection, Convert.ToInt32(chatmessagetypeString), 217, hubUser.UserId, robot.UserId, hubUser.Name, robot.Name, $"{hubUser.Name}向机器人{robot.Name}:========{message}======");
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(CustomerUser.UserId))
                        {
                            CustomerUser.UserId = robot.UserId;
                        }
                        if (string.IsNullOrEmpty(CustomerUser.Name))
                        {
                            CustomerUser.Name = robot.Name;
                        }

                        Console.WriteLine("请输入发送的消息:");
                        var message = Console.ReadLine();
                        await InvokeService(hubConnection, Convert.ToInt32(chatmessagetypeString), 217, hubUser.UserId, CustomerUser.UserId, hubUser.Name, CustomerUser.Name, $"{hubUser.Name}向客服{CustomerUser.Name}:========{message}======");
                    }
                  
                    break;
                default:
                    Console.WriteLine("输入有误");
                    break;
            }
        }
        else
        {
            //await GuestService(hubConnection, hubUser.UserId);
            //Console.WriteLine("客服请输入发送的消息:");
            //var message = Console.ReadLine();
            //await SendChat(hubConnection, 217, hubUser.UserId, GuestUser.UserId, hubUser.Name, GuestUser.Name, $"{hubUser.Name}向{GuestUser.Name}:========{message}======");
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

    var user = db.Queryable<AdminUser>().First(it => it.UserName == username);

    Console.WriteLine("登入成功");

    var claims = new Claim[]
           {
                 new Claim("sys_user_id", user.SysUserId.ToString()),
                 new Claim(ClaimTypes.Role,user.SysType),
                 new Claim(ClaimTypes.Name,user.UserName==null?"":user.UserName),
                 new Claim(ClaimTypes.NameIdentifier,user.SysUserId==null?"":user.SysUserId),
                 new Claim("IdentityId", user.SysUserId==null?"":user.SysUserId),
                 new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)),
           };

    var secretKey = "YPYBodsI43yTW11tjPYgpdp8LCfDeoa7FU88gDr0EFbE0z1vkyjPz3rd4BC4aGDP";

    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

    DateTime expires = DateTimeOffset.UtcNow.Add(TimeSpan.FromHours(10)).UtcDateTime;

    var token = new JwtSecurityToken(
                issuer: "66IN",
                audience: "In66",
                claims: claims,
                notBefore: DateTimeOffset.UtcNow.UtcDateTime,
                expires: expires,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

    string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

    //jwtToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiI2NklOIiwiaWF0IjoxNjcyOTc0ODk5LCJleHAiOjE2NzMwNDY4OTksIm5iZiI6MTY3Mjk3NDg5OSwianRpIjoibHpyNmtMVjN0Vld1MGw1MyIsInN1YiI6IjEwNDQwNSIsInBydiI6IjIzYmQ1Yzg5NDlmNjAwYWRiMzllNzAxYzQwMDg3MmRiN2E1OTc2ZjciLCJzeXNfdXNlcl9pZCI6IjkxODkzNjJlNDI5ZmM0ZmE3MTY4NzU3NTI3M2JhYTJkIn0.qRWglUCw745qYd65sI6JbR5oQCuyn6eyhiwRUD0l9x0";

    return jwtToken;
}

///// <summary>
///// CustomerService
///// </summary>
//async Task CustomerService(HubConnection hubConnection, string userid, int source)
//{
//    await hubConnection.SendAsync("CustomerService", userid, source);
//}

///// <summary>
///// GuestService
///// </summary>
//async Task GuestService(HubConnection hubConnection, string userid)
//{
//    await hubConnection.SendAsync("GuestService", userid);
//}

/// <summary>
/// InvokeService
/// </summary>
async Task InvokeService(HubConnection hubConnection, int chatmessagetype,int source, string senderid, string receiverid, string sender, string receiver, string message)
{
    await hubConnection.SendAsync("InvokeService", chatmessagetype,source, senderid, receiverid, sender, receiver, message);
}




