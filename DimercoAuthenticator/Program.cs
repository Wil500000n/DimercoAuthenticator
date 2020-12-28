using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimercoAuth
{
    class Program
    {
        static void Main(string[] args)
        {
            String Key = "wilson_w_huang@dimerco.com#A9773#Dimerco-BIT";
            AuthenticatorParas AuthPara = new AuthenticatorParas()
            {
                Private_Key = Key,
                Duration_Time = 30,
                Private_Key_Mobile = Base32.ToString(Encoding.UTF8.GetBytes(Key))
            };
            DimercoAuthenticator authenticator = new DimercoAuthenticator(AuthPara);
            var mobileKey = authenticator.GetMobilePhoneKey();
            while (true)
            {
                Console.WriteLine("Mobile PK：" + mobileKey);
                var code = authenticator.GenerateCode();
                Console.WriteLine("TOTP Code：" + code);
                Console.WriteLine("Countdown：" + AuthPara.Exprie_Sec);
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}
