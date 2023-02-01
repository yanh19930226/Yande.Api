using ConsoleApp.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SignalR.Models;
using SqlSugar;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

Console.WriteLine("Hello, World!");
SqlSugarClient db;

//const string ServerUrl = "http://localhost:5100";
const string ServerUrl = "http://imapi.66in.net";

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
        ConnectionString = "Data Source=121.43.34.54;port=3306;User ID=root;Password=66^^66;Database=66in;CharSet=utf8;sslmode=none;AllowLoadLocalInfile=true;",
        //ConnectionString = "Data Source=rm-bp1h4wtx57l4hi5o6ko.mysql.rds.aliyuncs.com;User ID=root;Password=ZDYyNDYxMDA5Zjc2Mzc1NTY=;Database=PRINT52;CharSet=utf8;sslmode=none;AllowLoadLocalInfile=true;",
        DbType = DbType.MySql,
        IsAutoCloseConnection = true,
        InitKeyType = InitKeyType.Attribute
    });

    Console.WriteLine("选择角色:1.kf1 2.yh 3.xl 4.yh1 6.yh2");

    var role = Console.ReadLine();

    if (role == "yh")
    {
        username = "yanh";

        password = "999";
        
        url =$"{ServerUrl}/chatHub/chatHub";

        await Task.Run(() => RunConnection(HttpTransportType.WebSockets, username, password, url, role));

    }
    else if (role == "kf1")
    {
        username = "kf1";

        password = "kf1";

        url = $"{ServerUrl}/chatHub/chatHub";

        await Task.Run(() => RunConnection(HttpTransportType.WebSockets, username, password, url, role));
    }
    else if (role == "yh1")
    {
        username = "yh1";

        password = "yh1";

        url = $"{ServerUrl}/chatHub/chatHub?source=221";

        await Task.Run(() => RunConnection(HttpTransportType.WebSockets, username, password, url, role));
    }
    else if (role == "yh2")
    {
        username = "yh2";

        password = "yh2";

        url = $"{ServerUrl}/chatHub/chatHub?source=221";

        await Task.Run(() => RunConnection(HttpTransportType.WebSockets, username, password, url, role));
    }
    else if (role == "xl")
    {
        username = "xiaoliang";

        password = "12345678";

        url = $"{ServerUrl}/chatHub/chatHub?source=221";

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
        robot.ConnectionId = message.ConnectionId;
        robot.UserId = message.UserId;
        robot.LoginTime = message.LoginTime;
        robot.Name = message.Name;
        Console.WriteLine($"机器人信息: {robot.UserId}\nCurrrentUserInfo: [{robot.Name}]\n");
    });
    //CurrrentUserInfo
    hubConnection.On<HubUser>("CurrrentUserInfo", (message) =>
    {
        hubUser.ServiceStatus = message.ServiceStatus;
        hubUser.ServiceStatusId = message.ServiceStatusId;
        hubUser.ConnectionId = message.ConnectionId;
        hubUser.UserId = message.UserId;
        hubUser.LoginTime = message.LoginTime;
        hubUser.Name = message.Name;
        Console.WriteLine($"当前用户连接Id: {message.UserId}\n当前用户: [{message.Name}]\n");
    });
    //CustomerUser
    hubConnection.On<HubUser>("CustomerService", (message) =>
    {
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

        if (role== "xl" || role == "yhl" || role == "yh2")
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

                    if ( chatmessagetype == (int)ChatMessageTypeEnum.CustomerService)
                    {
                        string message = string.Empty;

                        if (chatmessagetype == (int)ChatMessageTypeEnum.CustomerService)
                        {
                            Console.WriteLine("向机器人获取客服:");
                            message = "向机器人获取客服";
                        }

                        await InvokeService(hubConnection, chatmessagetype, 221, hubUser.UserId, robot.UserId, hubUser.Name, robot.Name, $"{hubUser.Name}向机器人{robot.Name}:========{message}======");
                    }
                    else if (chatmessagetype == (int)ChatMessageTypeEnum.Question)
                    {
                        string message = string.Empty;
                        Console.WriteLine("向机器人提问");
                        message = "人工";

                        await InvokeService(hubConnection, chatmessagetype, 221, hubUser.UserId, robot.UserId, hubUser.Name, robot.Name, message);

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
                        await InvokeService(hubConnection, Convert.ToInt32(chatmessagetypeString), 221, hubUser.UserId, CustomerUser.UserId, hubUser.Name, CustomerUser.Name, $"{hubUser.Name}向客服{CustomerUser.Name}:========{message}======");
                    }
                  
                    break;
                default:
                    Console.WriteLine("输入有误");
                    break;
            }
        }
        else
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

                    Console.WriteLine("请输入发送的消息:");
                    var message = Console.ReadLine();
                    await InvokeService(hubConnection, 1, 221, hubUser.UserId, "57db9babd35309f3cfb540a734e50b41", hubUser.Name, "57db9babd35309f3cfb540a734e50b41", $"{hubUser.Name}向57db9babd35309f3cfb540a734e50b41:========{message}======");

                    break;
                default:
                    Console.WriteLine("输入有误");
                    break;
            }
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
                 new Claim(ClaimTypes.Name,user.UserName==null?"":user.UserName),
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

    //jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzeXNfdXNlcl9pZCI6IjY5ZDQ1ZjJkLTllMDMtNGRhMS1hMDNhLTNlY2YwYjcxNjBiZSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiLkuKXovokiLCJuYmYiOjE2NzQwMjk0ODYsImV4cCI6MTY3NDA2NTQ4NiwiaXNzIjoiNjZJTiIsImF1ZCI6IkluNjYifQ.ldfXk1DcJ7cDTV6hGdKWs5yYE5GIMgqr6kSnaPkUVJs";
    //jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzeXNfdXNlcl9pZCI6IjJRRVQ4MDJBMThBWDNOV1oiLCJTeXNVc2VySWQiOiIyUUVUODAyQTE4QVgzTldaIiwiSWRlbnRpdHlJZCI6IjJRRVQ4MDJBMThBWDNOV1oiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTeXNBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJqb25pZW4xIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIyUUVUODAyQTE4QVgzTldaIiwiRW50ZXJwcmlzZU5hbWUiOiIiLCJMYXN0TG9naW5UaW1lIjoiMjAyMy8xLzE2IDE3OjUwOjExIiwiU2Vzc2lvbiI6IjJiODliMDNhLTViMzItNGJjNi05MDI5LTk3MzBmNDA3MTA1OCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvdXNlcmRhdGEiOiJ7XCJJZFwiOjIwNjQzLFwiU3lzVXNlcklkXCI6XCIyUUVUODAyQTE4QVgzTldaXCIsXCJVc2VyTmFtZVwiOlwiam9uaWVuMVwiLFwiUm9sZUlkXCI6XCI4NEZOS1VOWDAwNjg1UlM2XCIsXCJTeXNUeXBlXCI6XCJTeXNBZG1pblwiLFwiQ29ycElkXCI6XCJTeXNBZG1pblwiLFwiQXNjcmlwdGlvbkJ1c2luZXNzSWRcIjpudWxsLFwiRGVwYXJ0bWVudElkXCI6bnVsbCxcIkRlcGFydG1lbnRCZWxvbmdcIjpudWxsLFwiR3JvdXBJbmZvXCI6bnVsbCxcIlVzZXJQb3dlckxpc3RcIjpcIixcIixcIlBhc3N3b3JkXCI6XCI2MjQ0QjQ3NkRFQTExREFGNUNDNTNDMDU1MTA4REQ0QVwiLFwiUGljVXJsXCI6bnVsbCxcIlJlYWxOYW1lXCI6XCJcIixcIlRlbGVwaG9uZVwiOlwiMTUwNTg0MjQyODhcIixcIkVtYWlsXCI6bnVsbCxcIlFRXCI6bnVsbCxcIlNleFwiOlwiXCIsXCJBcmVhSWRcIjpudWxsLFwiU2Vzc2lvblwiOlwiMllMNk1JNkY1NkoyQUIwMFwiLFwiTGFzdExvZ2luVGltZVwiOlwiMjAyMy0wMS0xNlQxNjoxOTo0NVwiLFwiQWRkVGltZVwiOlwiMTkwMC0wMS0wMVQwMDowMDowMFwiLFwiVW5pdE5hbWVcIjpcIlwiLFwiV2VjaGF0SW5mb1wiOm51bGwsXCJEZXNpZ25OdW1iZXJcIjpudWxsLFwiUHJvbW90ZXJzXCI6bnVsbCxcIlByb21vdGVyc051bWJlclwiOm51bGwsXCJUb3RhbFNldHRsZW1lbnRNb25leVwiOjAuMDAsXCJCdXNpbmVzc0JhbGFuY2VcIjowLjAwfSIsImp0aSI6IjM3NTU5MjU4NzM5MTA0NSIsIm5iZiI6MTY3Mzg2MjYxMSwiZXhwIjoxNjczODk4NjExLCJpc3MiOiI2NklOIiwiYXVkIjoiSW42NiJ9.x9l7xMKYy9u5UAE2a2DfaIvQeHynMyp15ofs8iv3NBg";
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




