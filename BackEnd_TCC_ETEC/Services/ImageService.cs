using BackEndAplication.Data;

namespace BackEnd_TCC_ETEC.Services
{
    public class ImageService
    {
        public string GetImageService(string username)
        {
            var query = string.Format("SELECT ImageEncrypted FROM users WHERE username = '{0}'", username);
            var myConn = new MySQLConnectionWithValue();

            var imageResult = myConn.GetImageString(query);

            return imageResult.Result;
        }

        public string InsertImageService(string username, string image)
        {
            var query = string.Format("INSERT INTO users (ImageEncrypted) VALUES ('{0}') WHERE username = '{1}'", image, username);
            var myConn = new MySQLConnection();

            var insertImageResult = myConn.connectionDataBase(query);

            if (string.IsNullOrEmpty(insertImageResult))
            {
                return "Falha ao Inserir Imagem no Banco de Dados";
            }

            return "Sucesso ao Inserir Imagem";
        }

        public string UpdateImageService(string username, string image)
        {
            var query = string.Format("UPDATE users SET ImageEncrypted = '{0}' WHERE username = '{1}'", image, username);
            var myConn = new MySQLConnection();

            var insertImageResult = myConn.connectionDataBase(query);

            if (string.IsNullOrEmpty(insertImageResult))
            {
                return "Falha ao Atualizar Imagem no Banco de Dados";
            }

            return "Sucesso ao Atualizar Imagem";
        }
    }
}
