using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_firmware_management_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_firmware_management.CreateFirmware("dFI1lzE0EN2", "xQcjrfNLvQ", "10015488", "", "", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_firmware_management.UpdateFirmware("dFI1lzE0EN2", "xQcjrfNLvQ", "", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_firmware_management.QueryFirmware("dFI1lzE0EN2", "xQcjrfNLvQ", "", "10015488", "cd35c680b6d647068861f7fd4e79d3f5");
            Console.WriteLine("result = " + result);

            result = Aep_firmware_management.DeleteFirmware("dFI1lzE0EN2", "xQcjrfNLvQ", "", "10015488");
            Console.WriteLine("result = " + result);

            result = Aep_firmware_management.QueryFirmwareList("dFI1lzE0EN2", "xQcjrfNLvQ", "10015488");
            Console.WriteLine("result = " + result);


        }
    }
}