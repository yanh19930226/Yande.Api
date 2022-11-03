using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_operation_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_operation.QueryProductCount("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_operation.QueryProductInfos("dFI1lzE0EN2", "xQcjrfNLvQ", "");
            Console.WriteLine("result = " + result);

            result = Aep_operation.QueryActivatedDeviceCount("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_operation.QueryRegisteredDeviceCount("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_operation.QueryDeviceCountById("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_operation.QueryProductInfoByTime("dFI1lzE0EN2", "xQcjrfNLvQ", "", "");
            Console.WriteLine("result = " + result);

            result = Aep_operation.QueryCategoryBylevel("dFI1lzE0EN2", "xQcjrfNLvQ", "", "");
            Console.WriteLine("result = " + result);


        }
    }
}