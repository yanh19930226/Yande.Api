using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    public class Aep_command_modbus_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_command_modbus.CreateCommand("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_command_modbus.QueryCommand("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "", "10015488", "10015488test");
            Console.WriteLine("result = " + result);

            result = Aep_command_modbus.CancelCommand("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_command_modbus.QueryCommandList("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "10015488");
            Console.WriteLine("result = " + result);


        }
    }
}