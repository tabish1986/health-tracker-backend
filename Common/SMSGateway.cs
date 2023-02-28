using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Common
{
    public class SMSGateway
    {
        public void SendSMS(string sid, string token, string gatewayMobNo, string sms_message, string toMobNo)
        {
            TwilioClient.Init(sid, token);

            var message = MessageResource.Create(
                body: sms_message,
                from: new Twilio.Types.PhoneNumber(gatewayMobNo),
                to: new Twilio.Types.PhoneNumber(toMobNo)
            );
            
        }
    }
}
