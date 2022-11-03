using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_mq_sub_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_mq_sub.QueryServiceState("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_mq_sub.OpenMqService("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_mq_sub.QueryTopicInfo("dFI1lzE0EN2", "xQcjrfNLvQ", "");
            Console.WriteLine("result = " + result);

            result = Aep_mq_sub.QueryTopicCacheInfo("dFI1lzE0EN2", "xQcjrfNLvQ", "");
            Console.WriteLine("result = " + result);

            result = Aep_mq_sub.QueryTopics("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_mq_sub.QuerySubRules("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_mq_sub.ClosePushService("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);


        }
    }
}