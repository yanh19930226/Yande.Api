using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Usr_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Usr.SdkDownload("dFI1lzE0EN2", "xQcjrfNLvQ", "", "");
            Console.WriteLine("result = " + result);


        }
    }
}