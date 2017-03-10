using Lshp.OpenIDConnect.Data.Interface.Service;
using Lshp.OpenIDConnect.Util.ConfigManager;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.IdentityService
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private SmsSetting smsSetting;


        public AuthMessageSender(IOptions<ConfigEntry> option)
        {
            this.smsSetting = option.Value.SmsSettings;
        }

        public  Task SendEmailAsync(string email, string subject, string message)
        {
            return  Task.FromResult(0);
        }

        public async Task SendSmsAsync(string number, string message)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(smsSetting.BaseUri) })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{smsSetting.Sid}:{smsSetting.Token}")));

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("To",$"+{number}"),
                    new KeyValuePair<string, string>("From", smsSetting.From),
                    new KeyValuePair<string, string>("Body", message)
                 }); 

                await client.PostAsync(smsSetting.RequestUri, content).ConfigureAwait(false);
            }
        }
    }
}