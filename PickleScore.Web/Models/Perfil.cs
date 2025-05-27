using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickleScore.Web.Models
{
    public class Perfil
    {
        public int Id { get; set; }

        public Perfil() { }

        public string Nome { get; set; } // Nome do perfil
        public DateTime DataInsercao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
