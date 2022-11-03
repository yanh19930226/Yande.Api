using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    class Aep_cpe_management_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Aep_cpe_management.CpeAcsSelect("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.QueryDeviceList("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.GetAllDeviceInfo("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.deleteDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.CreateDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.OrderByParam("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.CreateProduct("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.UpdateCpeServer("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.InsertCpeServer("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.UpdateDevice("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.UpdateAcs("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.GetDeviceStatusHis("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.CreateAcs("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.FindAcs("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.FindDeviceCpeParam("dFI1lzE0EN2", "xQcjrfNLvQ", "", "10015488test");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.FindServerParamById("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.FindAcsById("dFI1lzE0EN2", "xQcjrfNLvQ", "");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.CpeServerSelect("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.CpeProductSelect("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.ParamSelect("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.DataTypeSelect("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.DeleteAcs("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.RebootDevice("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.FindDeviceById("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.FindCpeServer("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Aep_cpe_management.DeleteCpeServer("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);


        }
    }
}