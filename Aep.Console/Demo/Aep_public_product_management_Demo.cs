using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_public_product_management_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_public_product_management.QueryPublicByProductId("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_public_product_management.QueryPublicByPublicProductId("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_public_product_management.InstantiateProduct("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_public_product_management.QueryAllPublicProductList("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_public_product_management.QueryMyPublicProductList("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);


        }
    }
}