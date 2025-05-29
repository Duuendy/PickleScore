using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography.X509Certificates;

namespace PickleScore.Test
{
    [TestClass]
    public class CampeonatoTeste
    {
        [TestMethod]
        public void TESTE_CADASTRO_CAMPEONATO()
        {
            var nome = "Campeonato de Teste3";
            var local = "Olimpia";
            var dataInicio = "05/08/2025";
            var dataFim = "10/08/2025";
            var dataInsercao = DateTime.Now;
            var dataAlteracao = DateTime.Now;

            var campeonatoDAL = new PickleScore.Web.DAL.CampeonatoDAL();
            var campeonato = new PickleScore.Web.Models.Campeonato
            {
                Nome = nome,
                Local = local,
                DataInicio = DateTime.Parse(dataInicio),
                DataFim = DateTime.Parse(dataFim),
                DataInsercao = dataInsercao,
                DataAlteracao = dataAlteracao
            };

            campeonatoDAL.CadastrarCampeonato(campeonato);
            Assert.IsTrue(campeonato.Id == 0, "O campeonato não foi salvo corretamente.");
            Assert.AreEqual(nome, campeonato.Nome, "O nome do campeonato não foi salvo corretamente.");
            Assert.AreEqual(local, campeonato.Local, "O local do campeonato não foi salvo corretamente.");
        }

        [TestMethod]
        public void TESTE_CARREGAR_CAMPEONATO()
        {
            var campeonatoDAL = new PickleScore.Web.DAL.CampeonatoDAL();
            var campeonato = campeonatoDAL.CarregarCampeonato(1);
            Assert.IsNotNull(campeonato, "Campeonato não encontrado.");
            Assert.AreEqual(1, campeonato.Id, "O ID do campeonato não corresponde ao esperado.");
            Assert.IsNotNull(campeonato.Nome, "O nome do campeonato não foi carregado corretamente.");
        }

        [TestMethod]
        public void TESTE_ATUALIZAR_CAMPEONATO()
        {
            var campeonatoDAL = new PickleScore.Web.DAL.CampeonatoDAL();
            var campeonato = campeonatoDAL.CarregarCampeonato(3);
            Assert.IsNotNull(campeonato, "Campeonato não encontrado.");

            var datafimAlterado = DateTime.Parse("08/08/2025");
            campeonato.DataFim = datafimAlterado;
            campeonatoDAL.AtualizarCampeonato(campeonato);

            var campeonatoAtualizado = campeonatoDAL.CarregarCampeonato(3);
        }
    }
}
