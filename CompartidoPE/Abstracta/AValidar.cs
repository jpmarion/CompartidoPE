using CompartidoPE.Interface;

namespace CompartidoPE.Abstracta
{
    public abstract class AValidar<T>(IError error)
    {
        public AValidar<T>? _validador;

        public void ProximaValidacion(AValidar<T> validador)
        {
            _validador = validador;
        }
        public abstract Task<IError> Validacion(T valor);
        public async Task<IError> EsValido(T valor)
        {
            error = await Validacion(valor);
            if (error != null) return error;

            if (_validador != null)
            {
                return await _validador.EsValido(valor);
            }
            return null!;
        }
    }
}