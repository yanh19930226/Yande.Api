using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_order_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_order.refundCall("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_order.payCall("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_order.creditControl("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_order.Testxx("dFI1lzE0EN2", "xQcjrfNLvQ", "");
            Console.WriteLine("result = " + result);


        }
    }
}