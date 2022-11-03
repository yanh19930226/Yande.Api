using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_device_command_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_device_command.QueryCommandList("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "10015488", "10015488test");
            Console.WriteLine("result = " + result);

            result = Aep_device_command.QueryCommand("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "", "10015488", "10015488test");
            Console.WriteLine("result = " + result);

            result = Aep_device_command.CreateCommand("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_device_command.CancelCommand("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}