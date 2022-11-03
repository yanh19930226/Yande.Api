using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aeptest_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aeptest.testapi("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aeptest.testapi_local("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);


        }
    }
}