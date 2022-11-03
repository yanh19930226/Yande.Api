using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Distribute_it_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Distribute_it.CreatApp("dFI1lzE0EN2", "xQcjrfNLvQ", "", "{}");
            Console.WriteLine("result = " + result);

            result = Distribute_it.DeleteApp("dFI1lzE0EN2", "xQcjrfNLvQ", "", "", "", "{}");
            Console.WriteLine("result = " + result);

            result = Distribute_it.GetAddr("dFI1lzE0EN2", "xQcjrfNLvQ", "", "", "");
            Console.WriteLine("result = " + result);

            result = Distribute_it.getInfo("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);


        }
    }
}