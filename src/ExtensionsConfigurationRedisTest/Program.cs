using Extensions.Configuration.Redis;
using Microsoft.Extensions.Configuration;

namespace ExtensionsConfigurationRedisTest
{
    class Program
    {
        private static IConfiguration _configuration;

        static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var builder = new ConfigurationBuilder()
                .AddRedis("MyPrefix__myCacheKey",
                    cancellationTokenSource.Token, options =>
                    {
                        options.Server = "114.55.177.197";
                        options.OnReload = () =>
                        {
                            Console.WriteLine("============== Updated ============");
                            ReadConfig();
                        };
                    });


            _configuration = builder.Build();
            ReadConfig();


            Console.ReadLine();
            cancellationTokenSource.Cancel();
        }

        private static void ReadConfig()
        {
            Console.WriteLine($" ClassNo = { _configuration["ClassNo"] }");
            Console.WriteLine($" ClassDesc = { _configuration["ClassDesc"] }");

            Console.WriteLine("Students :");
            Console.Write(_configuration["Students:0:name"]);
            Console.WriteLine($" age: { _configuration["Students:0:age"]}");

            Console.Write(_configuration["Students:1:name"]);
            Console.WriteLine($" age: { _configuration["Students:1:age"]}");

            Console.Write(_configuration["Students:2:name"]);
            Console.WriteLine($" age: { _configuration["Students:2:age"]}");

        }
    }
}


