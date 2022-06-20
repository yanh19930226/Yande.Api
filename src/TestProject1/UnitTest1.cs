using System;
using Xunit;
using YandeSignSdk;
using YandeSignSdk.Models.Tests;

namespace TestProject1
{
    public class UnitTest1
    {
        private readonly YandeSignClient _client;

        public UnitTest1()
        {
            _client = new YandeSignClient(EnvEnum.Dev,"","","");
        }

        [Fact]
        public void Test1()
        {
            var para = new TestModel()
            {
                Id = "Test"
            };

            var res =  _client.PostRequestAsync(new TestReq(para));

            Assert.Equal("", "");
        }
    }
}
