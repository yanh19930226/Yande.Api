using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_device_control_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_device_control.QueryRemoteControlList("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "10015488");
            Console.WriteLine("result = " + result);

            result = Aep_device_control.CreateRemoteControl("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}