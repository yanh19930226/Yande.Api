using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_project_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_project.products("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_project.devices("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}