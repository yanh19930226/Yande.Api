using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Tr069_device_management_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Tr069_device_management.CreateDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Tr069_device_management.UpdateDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Tr069_device_management.DeleteDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Tr069_device_management.QueryDeviceList("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Tr069_device_management.QueryDevice("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Tr069_device_management.ListDeviceInfo("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);


        }
    }
}