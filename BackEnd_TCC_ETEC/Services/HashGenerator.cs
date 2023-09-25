using System.Security.Cryptography;
using System.Text;

namespace BackEnd_TCC_ETEC.Services
{
    public class HashGenerator
    {
        public string HashCreator(string pass)
        {
            var md5 = MD5.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(pass);
            byte[] hash = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
