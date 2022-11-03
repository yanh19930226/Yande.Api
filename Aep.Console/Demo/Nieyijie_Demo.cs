using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Nieyijie_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Nieyijie.TestQueryProductList("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);


        }
    }
}