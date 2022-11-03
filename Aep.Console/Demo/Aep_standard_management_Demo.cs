using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_standard_management_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_standard_management.QueryStandardModel("dFI1lzE0EN2", "xQcjrfNLvQ", "");
            Console.WriteLine("result = " + result);


        }
    }
}