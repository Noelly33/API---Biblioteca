using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Security
{
    public class Encriptacion
    {
        public static string EncriptarClave(string clave)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(clave));
                StringBuilder resultado = new StringBuilder();
                foreach (byte b in bytes)
                {
                    resultado.Append(b.ToString("x2"));
                }
                return resultado.ToString();
            }
        }
    }
}
