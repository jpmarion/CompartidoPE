namespace CompartidoPE.Interface
{
    public interface IValidar
    {
        Task<IError> Validar();
    }
}