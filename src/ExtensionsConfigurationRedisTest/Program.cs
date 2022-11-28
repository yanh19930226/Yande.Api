using Extensions.Configuration.Redis;
using Extensions.Configuration.RedisConfig;
using Microsoft.Extensions.Configuration;

namespace ExtensionsConfigurationRedisTest
{
    class Program
    {
        private static IConfiguration _configuration;

        static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            //var builder = new ConfigurationBuilder()
            //    .AddRedis("MyPrefix__myCacheKey",
            //        cancellationTokenSource.Token, options =>
            //        {
            //            options.Server = "114.55.177.197,connectTimeout=1000,connectRetry=1,syncTimeout=10000,abortConnect=false,DefaultDatabase=8";
            //            options.OnReload = () =>
            //            {
            //                Console.WriteLine("============== Updated ============");
            //                ReadConfig();
            //            };
            //        });


            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true);

            var options = new RedisOptions();

            options.ConnectionString = "114.55.177.197,connectTimeout=1000,connectRetry=1,syncTimeout=10000,abortConnect=false,DefaultDatabase=8";
            options.Key = "RedisConfig";

            builder = builder.AddRedisConfiguration(options,true);

            _configuration = builder.Build();

            ReadConfig();

            Console.ReadLine();

            cancellationTokenSource.Cancel();
        }

        private static void ReadConfig()
        {

            var IpPolicyPrefix = _configuration["IpPolicyPrefix"];

            Console.Write(IpPolicyPrefix);

            //Console.WriteLine($" ClassDesc = { _configuration["ClassDesc"] }");

            //Console.WriteLine("Students :");
            //Console.Write(_configuration["Students:0:name"]);
            //Console.WriteLine($" age: { _configuration["Students:0:age"]}");

            //Console.Write(_configuration["Students:1:name"]);
            //Console.WriteLine($" age: { _configuration["Students:1:age"]}");

            //Console.Write(_configuration["Students:2:name"]);
            //Console.WriteLine($" age: { _configuration["Students:2:age"]}");

        }
    }
}


