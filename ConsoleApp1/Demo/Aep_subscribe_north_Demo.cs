using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_subscribe_north_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_subscribe_north.GetSubscription("dFI1lzE0EN2", "xQcjrfNLvQ", "", "10015488", "cd35c680b6d647068861f7fd4e79d3f5");
            Console.WriteLine("result = " + result);

            result = Aep_subscribe_north.DeleteSubscription("dFI1lzE0EN2", "xQcjrfNLvQ", "", "10015488", "", "cd35c680b6d647068861f7fd4e79d3f5");
            Console.WriteLine("result = " + result);

            result = Aep_subscribe_north.GetSubscriptionsList("dFI1lzE0EN2", "xQcjrfNLvQ", "10015488", "", "", "cd35c680b6d647068861f7fd4e79d3f5", "");
            Console.WriteLine("result = " + result);

            result = Aep_subscribe_north.CreateSubscription("dFI1lzE0EN2", "xQcjrfNLvQ", "cd35c680b6d647068861f7fd4e79d3f5", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}