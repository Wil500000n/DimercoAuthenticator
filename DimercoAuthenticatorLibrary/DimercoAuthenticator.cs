using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DimercoAuth
{
    public class DimercoAuthenticator
    {
        AuthenticatorParas AuthPara = new AuthenticatorParas();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration">計時區間</param>
        /// <param name="key">密鑰，用來驗證</param>
        public DimercoAuthenticator(AuthenticatorParas _AuthPara)
        {
            AuthPara = _AuthPara;
        }

        /// <summary>
        /// get mobile key
        /// </summary>
        /// <returns></returns>
        public string GetMobilePhoneKey()
        {
            if (AuthPara.Private_Key_Mobile == null)
                throw new ArgumentNullException("Private_Key_Mobile");
            return AuthPara.Private_Key_Mobile;
        }

        /// <summary>
        /// Gen TOTP Code
        /// </summary>
        /// <returns>return OTP</returns>
        public string GenerateCode()
        {
            return GenerateHashedCode(AuthPara.Private_Key, AuthPara.Counter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PK">Private Key</param>
        /// <param name="iterationNumber">HMACSHA1 Key</param>
        /// <param name="digits">OTP length</param>
        /// <returns>OTP Code</returns>
        private string GenerateHashedCode(string PK, long iterationNumber, int digits = 6)
        {
            byte[] counter = BitConverter.GetBytes(iterationNumber);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(counter);
            byte[] key = Encoding.ASCII.GetBytes(PK);
            HMACSHA1 HMAC = new HMACSHA1(key, true);
            byte[] hash = HMAC.ComputeHash(counter);
            int offset = hash[hash.Length - 1] & 0xf;
            int binary = ((hash[offset] & 0x7f) << 24)
                | ((hash[offset + 1] & 0xff) << 16)
                | ((hash[offset + 2] & 0xff) << 8)
                | (hash[offset + 3] & 0xff);
            int password = binary % (int)Math.Pow(10, digits);
            return password.ToString(new string('0', digits));
        }
    }
}