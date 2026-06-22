namespace CompartidoPE.Abstracta
{
    public abstract class AEnum(string clave, string valor)
    {
        public string Clave { get; set; } = clave;
        public string Valor { get; set; } = valor;
    }
}