using CompartidoPE.Interface;

namespace CompartidoPE.Modelo
{
    public class Error : IError
    {
        public string NroError { get; set; }
        public string MsgError { get; set; }
    }
}
