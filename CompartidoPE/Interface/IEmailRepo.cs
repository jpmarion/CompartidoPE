namespace CompartidoPE.Interface
{
    public interface IEmailRepo
    {
         Task EnviarEmail(ICollection<string> emailDestino, string asunto, string cuerpo);
    }
}