// See https://aka.ms/new-console-template for more information
using AepSdk.Apis;

Console.WriteLine("Hello, World!");

string result = null;
result = Aep_device_status.QueryDeviceStatus("dFI1lzE0EN2", "xQcjrfNLvQ", "{}");
Console.WriteLine("result = " + result);