using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickleScore.Web.Models
{
    public class Nivel
    {
        public int Id { get; set; }
        public Nivel() { }
        public string Nome { get; set; } // Nome do nível
        public DateTime DataInsercao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}