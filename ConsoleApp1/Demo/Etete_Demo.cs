using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Etete_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Etete.sere("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Etete.QueryDeviceList("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "10015488");
            Console.WriteLine("result = " + result);


        }
    }
}