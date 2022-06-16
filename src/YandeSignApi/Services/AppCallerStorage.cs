using System.Collections.Generic;
using YandeSignApi.Models.Entities;

namespace YandeSignApi.Services
{
    public class AppCallerStorage
    {
        private AppCallerStorage()
        {

        }

        public static List<ApiCaller> ApiCallers = new List<ApiCaller>()
        {
            new ApiCaller()
            {
                Id = "1",
                Name = "我是1",
                PublickKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAyxqy0JN0rT+llRp+MKWN
px1I37iryAKtrM5ESG/r746zp5FE6/pT68mrypJCnh9FLtt/vjH1Ge3/kl/J7rGl
1d0w1K9drLd3AnBxuaYihY/GMtn/VzWz1uHEz/Ph2F0RMETHMtbfx/EkADyXCKCr
YHciJlZdJV3tJAPFPEKjq/ccGho2B4HQDFGdyvugQqcLfnjKl9BYZpMWtN0kCJ22
WK11N9GR0yB2LGExENGC0DGhh7wzDBIYfLD3K9m+5XYWq2irjeSPRWyOSU5ryLF6
lHKChBhVmxegV6/YXLE5av/TLAL+K9WBDHTRkxlK8fOVmvh6lybhqoAT3V4krsoU
cQIDAQAB"
            }
        };
    }
}
