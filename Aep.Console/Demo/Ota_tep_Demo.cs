using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Ota_tep_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Ota_tep.createTask("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Ota_tep.updateTask("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Ota_tep.deleteTask("dFI1lzE0EN2", "xQcjrfNLvQ", "", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}