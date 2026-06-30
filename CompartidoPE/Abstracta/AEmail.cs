using System.Net;
using System.Net.Mail;
using CompartidoPE.Interface;

namespace CompartidoPE.Abstracta
{
    public abstract class AEmai(string host,
                                 int port,
                                 string usuario,
                                 string contrasenia,
                                 bool habilitaSSL) : IEmailRepo
    {
        public async Task EnviarEmail(ICollection<string> emailDestino, string asunto, string cuerpo)
        {
            using var correo = new MailMessage();
            correo.From = new MailAddress(usuario);
            foreach (var email in emailDestino.Where(e => !string.IsNullOrWhiteSpace(e)))
            {
                correo.To.Add(email);
            }

            correo.Subject = asunto;
            correo.IsBodyHtml = true;
            correo.Body = cuerpo;
            correo.Priority = MailPriority.Normal;

            using var smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = host;
            smtp.Port = port;
            smtp.Credentials = new NetworkCredential(usuario, contrasenia);
            smtp.EnableSsl = habilitaSSL;

            await smtp.SendMailAsync(correo);
        }
    }
}
