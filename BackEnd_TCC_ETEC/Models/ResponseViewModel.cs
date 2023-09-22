namespace BackEndAplication.Models
{
    public class ResponseViewModel
    {
        public object Data { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }

    public class SimplifiedResponseViewModel
    {
        public object Data { get; set; }
        public string Mensagem { get; set; }
    }
}
