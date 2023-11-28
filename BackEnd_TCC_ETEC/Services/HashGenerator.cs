using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BackEnd_TCC_ETEC.Services
{
    public class HashGenerator
    {
        public string HashCreator(string pass)
        {
            //var md5 = MD5.Create();
            //byte[] bytes = System.Text.Encoding.ASCII.GetBytes(pass);
            //byte[] hash = md5.ComputeHash(bytes);

            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < hash.Length; i++)
            //{
            //    sb.Append(hash[i].ToString("X2"));
            //}

            //return sb.ToString();

            byte[] bytes = Encoding.UTF8.GetBytes(pass);
            var hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
