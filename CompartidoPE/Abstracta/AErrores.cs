using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace AuthPeDDD.Compartido.Abstracta
{
    public abstract class AErrores
    {
        private readonly Enum _enumValor;
        private FieldInfo _fieldInfo;
        public AErrores(Enum enumValor)
        {
            _enumValor = enumValor;
            _fieldInfo = _enumValor.GetType().GetField(_enumValor.ToString());
        }
        public string GetNroError()
        {
            StringBuilder NroError = new StringBuilder();
            NroError.Append(_fieldInfo.FieldType.Name.Replace("Enum", ""));
            NroError.Append(_enumValor.ToString("d"));

            return NroError.ToString();
        }
        public string GetDescription()
        {
            var description = _enumValor.ToString();

            if (_fieldInfo != null)
            {
                var attrs = _fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return description;
        }
    }
}