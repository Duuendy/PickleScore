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
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInsercao { get; set; }
        public int UsuarioInsercao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int UsuarioAlteracao { get; set; }
    }
}