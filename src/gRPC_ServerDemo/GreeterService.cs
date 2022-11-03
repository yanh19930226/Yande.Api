using Grpc.Core;
using GrpcService1;
using Hello;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace gRPC_ServerDemo.Services
{
    public class GreeterService : HelloService.HelloServiceBase
    {
        private readonly ILogger _logger;

        public GreeterService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GreeterService>();
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Sending hello to {request.Name}");

            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }
}