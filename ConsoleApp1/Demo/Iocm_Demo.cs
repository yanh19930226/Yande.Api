using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Iocm_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Iocm.delNbSubs("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Iocm.delNbSub("dFI1lzE0EN2", "xQcjrfNLvQ", "");
            Console.WriteLine("result = " + result);

            result = Iocm.getNbSubList("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Iocm.getNbSub("dFI1lzE0EN2", "xQcjrfNLvQ", "");
            Console.WriteLine("result = " + result);

            result = Iocm.nbSubProfData("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Iocm.getDeviceMsgOnePage("dFI1lzE0EN2", "xQcjrfNLvQ", "10015488test", "");
            Console.WriteLine("result = " + result);

            result = Iocm.nbSubProfDataV110("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);


        }
    }
}