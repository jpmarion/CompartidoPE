using CompartidoPE.Interface;
using CompartidoPE.Modelo;
using System.Collections;

namespace CompartidoPE.Abstracta
{
    public abstract class AEjecutarCU<T>
    {
        private readonly IResponse<T> _response;

        public AEjecutarCU(IResponse<T> response)
        {
            _response = response;
        }
        public abstract IList<T> Proceso();
        public abstract void BeginTransaction();
        public abstract void CommitTransaction();
        public abstract void RollbackTransaction();
        public IResponse<T> Ejecutar()
        {
            IError error = new Error();
            error.NroError = string.Empty;
            try
            {
                BeginTransaction();
                _response.Data = Proceso();
                _response.Error = error;
                CommitTransaction();
                return _response;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                if (ex.Data.Count != 0)
                {
                    foreach (DictionaryEntry item in ex.Data)
                    {
                        error.NroError = item.Key.ToString();
                        error.MsgError = item.Value.ToString();
                        _response.Error = error;
                    }
                    return _response;
                }
                else
                {
                    error.NroError = ex.StackTrace;
                    error.MsgError = ex.Message;
                    _response.Error = error;
                    return _response;
                }
            }
        }
    }
}
