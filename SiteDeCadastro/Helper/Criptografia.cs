using System.Security.Cryptography;
using System.Text;

namespace SiteDeCadastro.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string valor) //this torna o metodo uam extensão, sendo chamada atráves de um  "."
        {
            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(valor);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}
