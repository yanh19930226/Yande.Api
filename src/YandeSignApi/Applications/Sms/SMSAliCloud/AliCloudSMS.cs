﻿using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using Common;
using Microsoft.Extensions.Options;
using Sms;
using Sms.SMSAliCloud;
using System.Collections.Generic;
using YandeSignApi.Applications.Sms;

namespace SMS.AliCloud
{
    public class AliCloudSMS : ISMS
    {


        private readonly string accessKeyId;


        private readonly string accessKeySecret;



        public AliCloudSMS(IOptionsMonitor<SMSSetting> config)
        {
            accessKeyId = config.CurrentValue.AccessKeyId;
            accessKeySecret = config.CurrentValue.AccessKeySecret;
        }




        public bool SendSMS(string signName, string phone, string templateCode, Dictionary<string, string> templateParams)
        {
            try
            {
                string templateParamsJson = JsonHelper.ObjectToJson(templateParams);

                IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
                DefaultAcsClient client = new DefaultAcsClient(profile);
                CommonRequest request = new CommonRequest()
                {
                    Method = MethodType.POST,
                    Domain = "dysmsapi.aliyuncs.com"
                };
                request.AddQueryParameters("PhoneNumbers", phone);
                request.AddQueryParameters("SignName", signName);
                request.AddQueryParameters("TemplateCode", templateCode);
                request.AddQueryParameters("TemplateParam", templateParamsJson);
                request.Version = "2017-05-25";
                request.Action = "SendSms";

                CommonResponse response = client.GetCommonResponse(request);

                string retValue = System.Text.Encoding.Default.GetString(response.HttpResponse.Content);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
