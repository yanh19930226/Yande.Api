using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Testing_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Testing.apitest("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);


        }
    }
}