using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_tcp_command_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_tcp_command.commandToDevices("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}