using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace BackEnd_TCC_ETEC.Services
{
    public class EncryptService
    {
        public string ObjToByte(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(json);

            var finalString = Convert.ToBase64String(byteArray);
            return finalString;
        }
    }
}
