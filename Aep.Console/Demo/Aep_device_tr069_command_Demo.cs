using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_device_tr069_command_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_device_tr069_command.QueryCommandList("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_device_tr069_command.CreateCommand("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_device_tr069_command.QueryCommand("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);


        }
    }
}