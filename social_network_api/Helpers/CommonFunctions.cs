using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace social_network_api.Helpers
{
    public class CommonFunctions : ICommonFunctions
    {
        public string ComputeSha256Hash(string rawData)
        {
            StringBuilder builder = new StringBuilder();

            using (SHA256 hashComputer = SHA256.Create() )
            {
                // convert data to byte data
                byte[] byteRawData = hashComputer.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // conver byte raw data to string hex
                for (int i = 0; i < byteRawData.Length; i++)
                {
                    builder.Append(byteRawData[i].ToString("x2"));
                }
            }

            return builder.ToString();
        }
    }
}
