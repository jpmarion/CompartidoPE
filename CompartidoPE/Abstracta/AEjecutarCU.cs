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
            this._response = response;
        }
        public abstract IList<T> Proceso();
        public IResponse<T> Ejectuar()
        {
            IError error = new Error();
            error.NroError = string.Empty;
            try
            {
                _response.Data = Proceso();
                _response.Error = error;
                return _response;
            }
            catch (Exception ex)
            {
                if (ex.Data.Count !=0)
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
                    return _response;
                }
            }
        }
    }
}
