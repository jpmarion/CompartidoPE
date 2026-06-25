using CompartidoPE.Interface;

namespace CompartidoPE.Abstracta
{
    public abstract class AMediator<T>(IRequest request)
    {
        private readonly IRequest _request = request;
        public abstract Task<IResponse<T>> Ejecutar();
    }
}