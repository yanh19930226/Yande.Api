using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_rule_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_rule.saasCreateRule("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_rule.saasUpdateRule("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_rule.saasDeleteRuleEngine("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}