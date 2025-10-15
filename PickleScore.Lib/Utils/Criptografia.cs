using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCrypt.Net;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PickleScore.Web.Compatilhado
{
    public class Criptografia
    {
        public static string CriptografarSenha(string senha)
        {
            return BCryptNet.HashPassword(senha);
        }

        public static bool VerificarSenha(string senha, string hash)
        {
            return BCryptNet.Verify(senha, hash);
        }
    }
}