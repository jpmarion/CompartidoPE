using CompartidoPE.Interface;

namespace CompartidoPE.Modelo
{
    public class Response<T> : IResponse<T>
    {
        public IList<T> Data { get; set; }
        public IError Error { get; set; }
    }
}
