using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandeSignSdk.Models.Tests
{

    public class TestModel
    {
        public string Id { get; set; }
    }

    public class TestReq : BaseRequset<TestModel, BaseResponse<TestRep>>
    {
        public TestReq(TestModel data) : base(data)
        {
        }
        public override string Uri => "/test/test2";
    }

    public class TestRep
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
