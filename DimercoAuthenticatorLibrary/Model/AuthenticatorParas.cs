using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimercoAuth
{
    public class AuthenticatorParas
    {
        /// <summary>
        /// 計算區間長度 單位(秒)
        /// </summary>
        public long Duration_Time { get; set; }

        /// <summary>
        /// gen iteration key
        /// </summary>
        public long Counter
        {
            get
            {
                return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds / Duration_Time;
            }
        }

        /// <summary>
        /// Private_Key
        /// </summary>               
        public string Private_Key { get; set; }

        /// <summary>
        /// Private_Key Mobile
        /// Restore string
        /// </summary>
        public string Private_Key_Mobile { get; set; }
       
        /// <summary>
        /// 過期秒數
        /// </summary>
        public long Exprie_Sec
        {
            get
            {
                return (Duration_Time - (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds % Duration_Time);
            }
        }
    }
}