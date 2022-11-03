using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_device_group_management_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_device_group_management.CreateDeviceGroup("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_device_group_management.UpdateDeviceGroup("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_device_group_management.DeleteDeviceGroup("dFI1lzE0EN2", "xQcjrfNLvQ", "10015488", "");
            Console.WriteLine("result = " + result);

            result = Aep_device_group_management.QueryDeviceGroupList("dFI1lzE0EN2", "xQcjrfNLvQ", "10015488", "", "");
            Console.WriteLine("result = " + result);

            result = Aep_device_group_management.QueryGroupDeviceList("dFI1lzE0EN2", "xQcjrfNLvQ", "10015488", "", "");
            Console.WriteLine("result = " + result);

            result = Aep_device_group_management.UpdateDeviceGroupRelation("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}