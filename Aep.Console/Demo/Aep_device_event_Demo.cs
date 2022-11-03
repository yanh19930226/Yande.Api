using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    public class Aep_device_event_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_device_event.QueryDeviceEventTotal("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_device_event.QueryDeviceEventList("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}