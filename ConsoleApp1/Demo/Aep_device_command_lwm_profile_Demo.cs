using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_device_command_lwm_profile_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_device_command_lwm_profile.CreateCommandLwm2mProfile("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}