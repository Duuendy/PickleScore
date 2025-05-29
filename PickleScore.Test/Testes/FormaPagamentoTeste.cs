using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PickleScore.Test
{
    [TestClass]
    public class FormaPagamentoTeste
    {
        [TestMethod]
        public void TESTE_CADASTRAR_FORMA_PAGAMENTO()
        {
            var formaPagamentoDAL = new PickleScore.Web.DAL.FormaPagamentoDAL();
            var formaPagamento = new PickleScore.Web.Models.FormaPagamento
            {
                Nome = "PIX",
                DataInsercao = DateTime.Now,
                DataAlteracao = DateTime.Now
            };
            formaPagamentoDAL.CadastrarFormaPagamento(formaPagamento);
            Assert.IsTrue(formaPagamento.Id == 0, "A forma de pagamento não foi salva corretamente.");
            Assert.AreEqual("PIX", formaPagamento.Nome, "O nome da forma de pagamento não foi salvo corretamente.");
        }
    }
}
