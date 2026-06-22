using System.Security.Cryptography;
using System.Text;

namespace CompartidoPE.Herramientas
{
    public static class CifradoService
    {
        private static readonly byte[] _key = [];

        public static string Cifrar(string texto, string keyBase64)
        {
            byte[] key = Convert.FromBase64String(keyBase64);

            byte[] nonce = RandomNumberGenerator.GetBytes(12);
            byte[] textoBytes = Encoding.UTF8.GetBytes(texto);
            byte[] cifrado = new byte[textoBytes.Length];
            byte[] tag = new byte[16];

            using AesGcm aes = new(key, 16);
            aes.Encrypt(nonce, textoBytes, cifrado, tag);

            return string.Join(
                                ".",
                                Convert.ToBase64String(nonce),
                                Convert.ToBase64String(tag),
                                Convert.ToBase64String(cifrado));
        }

        public static string Descifra(string textoCifrado, string keyBase64)
        {
            byte[] key = Convert.FromBase64String(keyBase64);

            string[] partes = textoCifrado.Split('.');

            byte[] nonce = Convert.FromBase64String(partes[0]);
            byte[] tag = Convert.FromBase64String(partes[1]);
            byte[] cifrado = Convert.FromBase64String(partes[2]);

            byte[] textoBytes = new byte[cifrado.Length];

            using AesGcm aes = new(key, 16);
            aes.Decrypt(nonce, cifrado, tag, textoBytes);

            return Encoding.UTF8.GetString(textoBytes);
        }
    }
}