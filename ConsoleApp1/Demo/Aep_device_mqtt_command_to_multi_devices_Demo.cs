using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_device_mqtt_command_to_multi_devices_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_device_mqtt_command_to_multi_devices.commandToDevices("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}