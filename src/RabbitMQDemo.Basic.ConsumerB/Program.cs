using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQDemo.Basic.ConsumerB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

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
            //        channel.BasicQos(0, prefetchCount: 10, false);
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

        }
    }
}
