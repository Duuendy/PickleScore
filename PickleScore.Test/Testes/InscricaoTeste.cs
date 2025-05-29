using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PickleScore.Test
{
    [TestClass]
    public class InscricaoTeste
    {
        [TestMethod]
        public void TESTE_CADASTRAR_INSCRICAO()
        {
            var usuarioId = 1; // ID do usuário que está se inscrevendo
            var usuarioParceiroId = 0;
            var campeonatoId = 1; // ID do campeonato ao qual o usuário está se inscrevendo 
            var categoriaId = 1; // ID da categoria do usuário
            var nivelId = 1; // ID do nível do usuário
            var faixaEtariaId = 1; // ID da faixa etária do usuário
            var valor = 100; // Valor da inscrição
            var formaPagamentoId = 1; // Forma de pagamento
            var DataInscricao = DateTime.Now;
            var dataInsercao = DateTime.Now;
            var dataAlteracao = DateTime.Now;

            var inscricaoDAL = new PickleScore.Web.DAL.InscricaoDAL();
            var inscricao = new PickleScore.Web.Models.Inscricao
            {
                UsuarioId = usuarioId,
                UsuarioParceiroId = usuarioParceiroId,
                CampeonatoId = campeonatoId,
                CategoriaId = categoriaId,
                NivelId = nivelId,
                FaixaEtariaId = faixaEtariaId,
                Valor = valor,
                FormaPagamentoId = formaPagamentoId,
                DataInscricao = DataInscricao,
                DataInsercao = dataInsercao,
                DataAlteracao = dataAlteracao
            };

            inscricaoDAL.CadastrarInscricao(inscricao);
            Assert.IsTrue(inscricao.Id == 0, "A inscrição não foi salva corretamente.");
            Assert.AreEqual(usuarioId, inscricao.UsuarioId, "O ID do usuário não foi salvo corretamente.");

        }
    }
}
