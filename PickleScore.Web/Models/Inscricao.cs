using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickleScore.Web.Models
{
    public class Inscricao
    {
        public int Id { get; set; }
        public Inscricao() { }
        public int UsuarioId { get; set; }
        public int UsuarioParceiroId { get; set; }
        public int CampeonatoId { get; set; }
        public int CategoriaId { get; set; }
        public int NivelId { get; set; }
        public int FaixaEtariaId { get; set; }
        public int Valor { get; set; } // Valor da inscrição
        public string FormaPagamento { get; set; } // "Cartão de Crédito", "Boleto", "Pix"
        public DateTime DataInicio { get; set; } // Data de início do campeonato
        public DateTime DataFim { get; set; } // Data de fim do campeonato
        public DateTime DataInsercao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}