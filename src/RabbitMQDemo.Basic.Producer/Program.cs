using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQDemo.Basic.Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region 简单队列模式
            //var connFactory = new ConnectionFactory
            //{
            //    HostName = "139.198.178.61",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using (var conn = connFactory.CreateConnection())
            //{
            //    using (var channel = conn.CreateModel())
            //    {
            //        var queueName = "helloworld";
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //        while (true)
            //        {
            //            Console.WriteLine("消息内容(exit退出):");
            //            var message = Console.ReadLine();
            //            if (message.Trim().ToLower() == "exit")
            //            {
            //                break;
            //            }

            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            //            Console.WriteLine("消息内容发送完毕:" + message);
            //        }
            //    }
            //}
            #endregion

            #region Worker队列模式
            //var connFactory = new ConnectionFactory
            //{
            //    HostName = "139.198.178.61",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using (var conn = connFactory.CreateConnection())
            //{
            //    using (var channel = conn.CreateModel())
            //    {
            //        var queueName = "worker";
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //        while (true)
            //        {
            //            Console.WriteLine("消息内容(exit退出):");
            //            var message = Console.ReadLine();
            //            if (message.Trim().ToLower() == "exit")
            //            {
            //                break;
            //            }

            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            //            Console.WriteLine("消息内容发送完毕:" + message);
            //        }
            //    }
            //}
            #endregion

            #region Exchange发布订阅模式

            var connFactory = new ConnectionFactory
            {
                HostName = "139.198.178.61",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            using (var conn = connFactory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    var exchangeName = "publishsubscribe_exchange";
                    channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");
                    while (true)
                    {
                        Console.WriteLine("消息内容(exit退出):");
                        var message = Console.ReadLine();
                        if (message.Trim().ToLower() == "exit")
                        {
                            break;
                        }

                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: body);
                        Console.WriteLine("消息内容发送完毕:" + message);
                    }
                }
            }

            #endregion

        }
    }
}
