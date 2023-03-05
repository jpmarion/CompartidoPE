using System;
using System.ComponentModel;
using System.Text;

namespace AuthPeDDD.Compartido.Abstracta
{
    public abstract class AErrores
    {
        private readonly Enum _enumValor;
        public AErrores(Enum enumValor)
        {
            this._enumValor = enumValor;
        }
        public string GetNroError()
        {
            StringBuilder NroError = new StringBuilder();
            NroError.Append(_enumValor.GetType().ToString());
            NroError.Append(_enumValor.ToString());
            return NroError.ToString();
        }
        public string GetDescription()
        {
            var description = _enumValor.ToString();
            var fieldInfo = _enumValor.GetType().GetField(_enumValor.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return description;
        }
    }
}