using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
            //        var exchangeName = "publishsubscribe_exchange";
            //        channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");
            //        while (true)
            //        {
            //            Console.WriteLine("消息内容(exit退出):");
            //            var message = Console.ReadLine();
            //            if (message.Trim().ToLower() == "exit")
            //            {
            //                break;
            //            }

            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: body);
            //            Console.WriteLine("消息内容发送完毕:" + message);
            //        }
            //    }
            //}

            #endregion

            #region Exchange路由模式
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
            //        var exchangeName = "routing_exchange";
            //        channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
            //        while (true)
            //        {
            //            Console.WriteLine("消息RoutingKey(warning or info):");
            //            var routingKey = Console.ReadLine();
            //            Console.WriteLine("消息内容(exit退出):");
            //            var message = Console.ReadLine();
            //            if (message.Trim().ToLower() == "exit")
            //            {
            //                break;
            //            }

            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);
            //            Console.WriteLine("消息内容发送完毕:" + message);
            //        }
            //    }
            //}
            #endregion

            #region Exchange通配符模式
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
            //        var exchangeName = "topics_exchange";
            //        channel.ExchangeDeclare(exchange: exchangeName, type: "topic");
            //        while (true)
            //        {
            //            Console.WriteLine("消息RoutingKey:");
            //            var routingKey = Console.ReadLine();

            //            Console.WriteLine("消息内容(exit退出):");
            //            var message = Console.ReadLine();
            //            if (message.Trim().ToLower() == "exit")
            //            {
            //                break;
            //            }

            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);
            //            Console.WriteLine("消息内容发送完毕:" + message);
            //        }
            //    }
            //}
            #endregion

            #region 生产者事务模式确保消息投递到队列
            //var connFactory = new ConnectionFactory
            //{
            //    HostName = "139.198.156.173",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using (var conn = connFactory.CreateConnection())
            //{
            //    using (var channel = conn.CreateModel())
            //    {
            //        var queueName = "helloworld_tx";
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        channel.TxSelect();

            //        while (true)
            //        {
            //            Console.WriteLine("消息内容(exit退出):");
            //            var message = Console.ReadLine();
            //            if (message.Trim().ToLower() == "exit")
            //            {
            //                break;
            //            }

            //            try
            //            {
            //                var body = Encoding.UTF8.GetBytes(message);
            //                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            //                if (message.Trim().ToLower() == "9527")
            //                {
            //                    throw new Exception("触发异常");
            //                }
            //                channel.TxCommit();
            //                Console.WriteLine("消息内容发送完毕:" + message);
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine("消息发送出现异常:" + ex.Message);
            //                channel.TxRollback();
            //            }
            //        }
            //    }
            //}
            #endregion

            #region 生产者确认模式确保消息投递到队列
            //单条确认
            //var connFactory = new ConnectionFactory
            //{
            //    HostName = "139.198.156.173",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using (var conn = connFactory.CreateConnection())
            //{
            //    using (var channel = conn.CreateModel())
            //    {
            //        var queueName = "helloworld_producerack_singleconfirm";
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        channel.ConfirmSelect();

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
            //            Console.WriteLine(channel.WaitForConfirms(new TimeSpan(0, 0, 1)) ? "消息发送成功" : "消息发送失败");
            //        }
            //    }
            //}

            //批量确认
            //var connFactory = new ConnectionFactory
            //{
            //    HostName = "139.198.156.173",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using (var conn = connFactory.CreateConnection())
            //{
            //    using (var channel = conn.CreateModel())
            //    {
            //        var queueName = "helloworld_producerack_batchconfirm";
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        channel.ConfirmSelect();

            //        while (true)
            //        {
            //            for (int i = 0; i < 3; i++)
            //            {
            //                Console.WriteLine("消息内容(exit退出):");
            //                var message = Console.ReadLine();
            //                if (message.Trim().ToLower() == "exit")
            //                {
            //                    break;
            //                }

            //                var body = Encoding.UTF8.GetBytes(message);
            //                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            //                Console.WriteLine("消息内容发送完毕:" + message);
            //            }

            //            Console.WriteLine(channel.WaitForConfirms() ? "所有消息内容发送完毕" : "存在消息发送失败");
            //        }
            //    }
            //}
            //异步确认模式
            //var connFactory = new ConnectionFactory
            //{
            //    HostName = "139.198.156.173",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using (var conn = connFactory.CreateConnection())
            //{
            //    using (var channel = conn.CreateModel())
            //    {
            //        var queueName = "helloworld_producerack_asyncconfirm";
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        channel.ConfirmSelect();

            //        channel.BasicAcks += new EventHandler<BasicAckEventArgs>((o, basic) =>
            //        {
            //            Console.WriteLine($"调用ack回调方法: DeliveryTag: {basic.DeliveryTag};Multiple: {basic.Multiple}");
            //        });
            //        channel.BasicNacks += new EventHandler<BasicNackEventArgs>((o, basic) =>
            //        {
            //            Console.WriteLine($"调用Nacks回调方法; DeliveryTag: {basic.DeliveryTag};Multiple: {basic.Multiple}");
            //        });

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

            #region 消费者可靠接收
            //var connFactory = new ConnectionFactory
            //{
            //    HostName = "139.198.156.173",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using (var conn = connFactory.CreateConnection())
            //{
            //    using (var channel = conn.CreateModel())
            //    {
            //        var queueName = "helloworld_consumerack";
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        Console.WriteLine("任意键开始发布消息:");
            //        var pause = Console.ReadLine();
            //        for (int i = 0; i < 100; i++)
            //        {
            //            var message = $"发送的消息{i}";
            //            Console.WriteLine(message); 
            //            if (message.Trim().ToLower() == "exit")
            //            {
            //                break;
            //            }
            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            //        }
            //        Console.WriteLine("消息内容发送完毕!");
            //        Console.ReadKey();
            //    }
            //}
            #endregion

            #region 消费者可靠接收自动确认消息丢失
            //var connFactory = new ConnectionFactory
            //{
            //    HostName = "139.198.156.173",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using (var conn = connFactory.CreateConnection())
            //{
            //    using (var channel = conn.CreateModel())
            //    {
            //        var queueName = "helloworld_consumerack";
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //        Console.WriteLine("任意键开始发布消息:");
            //        var pause = Console.ReadLine();
            //        for (int i = 0; i < 100; i++)
            //        {
            //            var message = $"消息内容:{i}";
            //            Console.WriteLine($"发布消息:{message}");
            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

            //        }
            //        Console.WriteLine("消息内容发送完毕");
            //        Console.ReadKey();
            //    }
            //}
            #endregion

            #region Mq消息可靠存储机制

            //mandatory为true时，消息则返回给生产者
            //var connFactory = new ConnectionFactory
            //{
            //    HostName = "139.198.156.173",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using (var conn = connFactory.CreateConnection())
            //{
            //    using (var channel = conn.CreateModel())
            //    {
            //        var exchangeName = "mandatory_publishsubscribe_exchange";
            //        channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");

            //        channel.BasicReturn += new EventHandler<RabbitMQ.Client.Events.BasicReturnEventArgs>((sender, e) =>
            //        {
            //            var message = Encoding.UTF8.GetString(e.Body.ToArray());
            //            Console.WriteLine($"收到回退消息：{message}");
            //            //匹配不到交换机处理消息


            //        });

            //        while (true)
            //        {
            //            Console.WriteLine("消息内容(exit退出):");
            //            var message = Console.ReadLine();
            //            if (message.Trim().ToLower() == "exit")
            //            {
            //                break;
            //            }

            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: exchangeName, routingKey: "", mandatory: true, basicProperties: null, body: body);
            //            Console.WriteLine("消息内容发送完毕:" + message);
            //        }
            //    }
            //}

            //mandatory为false时，备份交换机并绑定一个队列，用于存储被丢弃的消息
            //var connFactory = new ConnectionFactory
            //{
            //    HostName = "139.198.156.173",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using (var conn = connFactory.CreateConnection())
            //{
            //    using (var channel = conn.CreateModel())
            //    {
            //        var exchangeName = "aedemo_publishsubscribe_exchange";
            //        var alternateExchangeName = "aedemo_ae_publishsubscribe_exchange";
            //        var arguments = new Dictionary<string, object>
            //         {
            //             { "alternate-exchange", alternateExchangeName }
            //         };
            //        channel.ExchangeDeclare(exchange: exchangeName, type: "fanout", arguments: arguments);
            //        channel.ExchangeDeclare(exchange: alternateExchangeName, type: "fanout");

            //        var alternateExchangeQueueName = alternateExchangeName + "_worker";
            //        channel.QueueDeclare(queue: alternateExchangeQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        channel.QueueBind(queue: alternateExchangeQueueName, exchange: alternateExchangeName, routingKey: "");

            //        while (true)
            //        {
            //            Console.WriteLine("消息内容(exit退出):");
            //            var message = Console.ReadLine();
            //            if (message.Trim().ToLower() == "exit")
            //            {
            //                break;
            //            }

            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: exchangeName, routingKey: "", mandatory: false, basicProperties: null, body: body);
            //            Console.WriteLine("消息内容发送完毕:" + message);
            //        }
            //    }
            //}

            #endregion

            #region 死信队列

            //var connFactory = new ConnectionFactory
            //{
            //    HostName = "139.198.156.173",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using (var conn = connFactory.CreateConnection())
            //{
            //    using (var channel = conn.CreateModel())
            //    {
            //        //死信交换机和死信队列
            //        var dlxExchangeName = "dlxroutingkey_exchange";
            //        channel.ExchangeDeclare(exchange: dlxExchangeName, type: "direct", durable: false, autoDelete: false, arguments: null);

            //        var dlxQueueName1 = "dlx_queue1";
            //        channel.QueueDeclare(queue: dlxQueueName1, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        channel.QueueBind(queue: dlxQueueName1, exchange: dlxExchangeName, routingKey: "waring");

            //        var dlxQueueName2 = "dlx_queue2";
            //        channel.QueueDeclare(queue: dlxQueueName2, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        channel.QueueBind(queue: dlxQueueName2, exchange: dlxExchangeName, routingKey: "info");

            //        var dlxQueueName3 = "dlx_queue1";
            //        channel.QueueDeclare(queue: dlxQueueName3, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        channel.QueueBind(queue: dlxQueueName3, exchange: dlxExchangeName, routingKey: "error");

            //        //常规队列
            //        var queueName = "normalmessage_queue";
            //        var arguments = new Dictionary<string, object>
            //        {
            //            { "x-message-ttl", 10000},
            //            { "x-dead-letter-exchange", dlxExchangeName },
            //            { "x-dead-letter-routing-key", "info" }
            //        };

            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: arguments);

            //        while (true)
            //        {
            //            Console.WriteLine("消息内容(exit退出):");
            //            var message = Console.ReadLine();
            //            if (message.Trim().ToLower() == "exit")
            //            {
            //                break;
            //            }

            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: false, basicProperties: null, body: body);
            //            Console.WriteLine("消息内容发送完毕:" + message);
            //        }
            //    }
            //}

            #endregion

            #region 优先级队列

            var connFactory = new ConnectionFactory
            {
                HostName = "139.198.156.173",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            using (var conn = connFactory.CreateConnection())
            {
                //using (var channel = conn.CreateModel())
                //{
                //    var queueName = "priorityqueue";
                //    var arguments = new Dictionary<string, object>
                //    {
                //        { "x-max-priority", 10 }
                //    };
                //    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: arguments);

                //    for (int i = 0; i < 10; i++)
                //    {
                //        var message = "第" + (i + 1) + "条消息";
                //        byte priority = 0;

                //        if (i < 3)
                //        {
                //            message += "-info";
                //            priority = 1;
                //        }

                //        else if (i < 6 && i >= 3)
                //        {
                //            message += "-warn";
                //            priority = 2;
                //        }
                //        else
                //        {
                //            message += "-error";
                //            priority = 3;
                //        }
                //        var body = Encoding.UTF8.GetBytes(message);
                //        var basicProperties = channel.CreateBasicProperties();
                //        basicProperties.Priority = priority;
                //        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: basicProperties, body: body);
                //        Console.WriteLine("消息内容发送完毕:" + message);
                //    }

                //    Console.ReadKey();
                //}
            }
            #endregion

        }
    }
}
