using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_device_status_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_device_status.QueryDeviceStatus("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_device_status.getDeviceStatusHisInPage_test("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_device_status.getDeviceStatusHisInTotal_test("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_device_status.getDeviceStatusHisInTotal("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_device_status.getDeviceStatusHisInPage("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_device_status.QueryDeviceStatusList("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}