using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Iodm_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Iodm.nbSubManageData("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}