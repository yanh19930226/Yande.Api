using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_nb_device_management_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_nb_device_management.BatchCreateNBDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_nb_device_management.CreateDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_nb_device_management.UpdateDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "10015488test", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_nb_device_management.QueryDeviceList("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5");
            Console.WriteLine("result = " + result);

            result = Aep_nb_device_management.QueryDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "10015488test", "10015488");
            Console.WriteLine("result = " + result);

            result = Aep_nb_device_management.DeleteDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "10015488", "");
            Console.WriteLine("result = " + result);

            result = Aep_nb_device_management.BatchCancelDevices("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_nb_device_management.DeleteDeviceByImei("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}