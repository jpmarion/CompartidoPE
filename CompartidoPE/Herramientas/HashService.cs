using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace CompartidoPE.Herramientas
{
    public static class HashService<TUser> where TUser : class
    {
        private static readonly PasswordHasher<TUser> _passwordHasher = new();

        public static string CrearPasswordHash(TUser user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }
        public static bool VerificarPassword(TUser user, string passwordHash, string password)
        {
            PasswordVerificationResult resultado = _passwordHasher.VerifyHashedPassword(user, passwordHash, password);

            return resultado == PasswordVerificationResult.Success || resultado == PasswordVerificationResult.SuccessRehashNeeded;
        }
        public static string CrearHashBusqueda(string valor)
        {
            string valorNormalizado = Normalizar(valor);

            byte[] bytes = Encoding.UTF8.GetBytes(valorNormalizado);
            byte[] hash = SHA256.HashData(bytes);

            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        private static string Normalizar(string valor)
        {
            return valor.Trim().ToLowerInvariant();
        }
    }
}