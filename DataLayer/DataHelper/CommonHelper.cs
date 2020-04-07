using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataHelper
{
   public class CommonHelper
    {
        public static string Encrypt(string strPassword)
        {
            byte[] sByBuf = Encoding.ASCII.GetBytes(strPassword);
            for (int nCtr = 0; nCtr < sByBuf.Length; nCtr++)
                sByBuf[nCtr] = Convert.ToByte(Convert.ToInt64(sByBuf[nCtr]) + 4);
            strPassword = Encoding.ASCII.GetString(sByBuf);
            return strPassword;
        }

        public static string Decrypt(string strPassword)
        {
            byte[] sByBuf = Encoding.ASCII.GetBytes(strPassword);
            for (int nCtr = 0; nCtr < sByBuf.Length; nCtr++)
                sByBuf[nCtr] = Convert.ToByte(Convert.ToInt64(sByBuf[nCtr]) - 4);
            strPassword = Encoding.ASCII.GetString(sByBuf);
            return strPassword;
        }
    }
}
