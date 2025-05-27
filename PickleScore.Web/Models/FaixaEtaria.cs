using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickleScore.Web.Models
{
    public class FaixaEtaria
    {
        public int Id { get; set; }
        public FaixaEtaria() { }
        public string Nome { get; set; } // Nome da faixa etária
        public DateTime DataInsercao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}