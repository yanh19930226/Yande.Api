using AepSdk.Apis;
using System;

namespace AepSdk.Demo
{
    public class Webctdfs_Demo
    {
        public static void Demo()
        {
            string result = null;
            
            result = Webctdfs.webctdfs_fileInfos_get("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Webctdfs.webctdfs_file_post("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
            Console.WriteLine("result = " + result);

            result = Webctdfs.webctdfs_file_get("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);

            result = Webctdfs.webctdfs_file_delete("dFI1lzE0EN2", "xQcjrfNLvQ");
            Console.WriteLine("result = " + result);


        }
    }
}