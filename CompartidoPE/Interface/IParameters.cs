namespace CompartidoPE.Interface
{
    public interface IParameters<T>
    {
        public IResponse<T> Response { get; set; }
        public IError? Error { get; set; }
        public IValidar? Validar { get; set; }
    }
}