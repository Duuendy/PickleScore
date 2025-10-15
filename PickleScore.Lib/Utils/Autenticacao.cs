using PickleScore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickleScore.Lib.Utils
{
    public class Autenticacao
    {
        public bool Sucesso {  get; set; }
        public bool Erro{ get; set; }
        public Usuario Usuario { get; set; }
    }
}
