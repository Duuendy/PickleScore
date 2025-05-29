using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PickleScore.Test
{
    [TestClass]
    public class FaixaEtariaTeste
    {
        [TestMethod]
        public void TESTE_CADASTRAR_FAIXAETARIA()
        {
            var nome = "70+";
            var faixaEtariaDAL = new PickleScore.Web.DAL.FaixaEtariaDAL();
            var faixaEtaria = new PickleScore.Web.Models.FaixaEtaria();
            faixaEtaria.Nome = nome;
            faixaEtaria.DataInsercao = DateTime.Now;
            faixaEtaria.DataAlteracao = DateTime.Now;
            faixaEtariaDAL.SalvarFaixaEtaria(faixaEtaria);
            Assert.IsTrue(faixaEtaria.Id == 0, "A faixa etária não foi salva corretamente.");
            Assert.AreEqual(nome, faixaEtaria.Nome, "O nome da faixa etária não foi salvo corretamente.");
        }
    }
}
