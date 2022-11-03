using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_statistic_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_statistic.apiReport("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);


        }
    }
}