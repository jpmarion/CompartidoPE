using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace AuthPeDDD.Compartido.Abstracta
{
    public abstract class AErrores
    {
        protected AErrores(string nroError, string msgError)
        {
            _nroError = nroError;
            MsgError = msgError;
        }

        private string? _nroError;

        public string NroError
        {
            get { return $"{GetType().Name}{_nroError}"; }
            set => _nroError = value;
        }
        public string? MsgError { get; set; }
    }
}