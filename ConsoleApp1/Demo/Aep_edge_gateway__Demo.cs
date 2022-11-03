using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_edge_gateway__Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_edge_gateway_.QueryEdgeInstanceDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "");
            Console.WriteLine("result = " + result);

            result = Aep_edge_gateway_.DeleteEdgeInstanceDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_edge_gateway_.DeleteEdgeInstance("dFI1lzE0EN2", "xQcjrfNLvQ", "");
            Console.WriteLine("result = " + result);

            result = Aep_edge_gateway_.AddEdgeInstanceDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_edge_gateway_.AddEdgeInstanceDrive("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_edge_gateway_.CreateEdgeInstance("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_edge_gateway_.EdgeInstanceDeploy("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}