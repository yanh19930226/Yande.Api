using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQDemo.Basic.Consumer
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

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            var message = ea.Body;
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()));
            //            ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
            //        };

            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
            //    }
            //} 
            #endregion

            #region Worker模式
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
            //        channel.BasicQos(0, 1, false);
            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            Thread.Sleep(10000);
            //            var message = ea.Body;
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()) + DateTime.Now.ToString("hh:mm:ss"));
            //            ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
            //        };

            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
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
            //        var queueName = exchangeName + "_worker_1";
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");

            //        channel.BasicQos(0, 10, false);

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            Thread.Sleep(1000);
            //            var message = ea.Body;
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()) + DateTime.Now.ToString("hh:mm:ss"));
            //            ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
            //        };

            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
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
            //        var queueName = exchangeName + "_worker_1";
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //        var routingKey1 = "warning";
            //        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey1);
            //        var routingKey2 = "info";
            //        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey2);

            //        channel.BasicQos(0, 10, false);

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            Thread.Sleep(1000);
            //            var message = ea.Body;
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()) + DateTime.Now.ToString("hh:mm:ss"));
            //            ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
            //        };

            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
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
            //        var queueName = exchangeName + "_worker_2";
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //        var routingKey1 = "index.*";
            //        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey1);
            //        var routingKey2 = "#.created.#";
            //        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey2);

            //        channel.BasicQos(0, 10, false);

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            Thread.Sleep(1000);
            //            var message = ea.Body;
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()) + DateTime.Now.ToString("hh:mm:ss"));
            //            ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
            //        };

            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
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

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            var message = ea.Body;
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()));
            //            ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
            //        };

            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
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

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            var message = ea.Body;
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()));
            //            ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
            //        };

            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
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

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            var message = ea.Body;
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()));
            //            ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
            //        };

            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
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

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            var message = ea.Body;
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()));
            //            ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
            //        };

            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
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
            //        channel.BasicQos(0, 5, false);

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            Thread.Sleep(10000);
            //            var message = ea.Body;
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()));

            //            ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
            //        };

            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

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
            //        channel.BasicQos(0, 5, false);

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            Thread.Sleep(1000);
            //            var message = ea.Body;
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()));

            //            if (Encoding.UTF8.GetString(message.ToArray()).Contains("50"))
            //            {
            //                Console.WriteLine("异常");
            //                throw new Exception("模拟异常");
            //            }

            //           ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
            //        };

            //        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            //        Console.ReadKey();
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
                using (var channel = conn.CreateModel())
                {
                    var queueName = "priorityqueue";
                    var arguments = new Dictionary<string, object>
                     {
                         { "x-max-priority", 10 }
                     };
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: arguments);
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var message = ea.Body;
                        Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message.ToArray()));
                        ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
                    };

                    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

                    Console.ReadKey();
                }
            }
            #endregion
        }
    }
}
