using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography.X509Certificates;

namespace PickleScore.Test
{
    [TestClass]
    public class NivelTeste
    {
        [TestMethod]
        public void TESTE_CADASTRAR_NIVEL()
        {
            var nivelDAL = new PickleScore.Web.DAL.NivelDAL();
            var nivel = new PickleScore.Web.Models.Nivel
            {
                Nome = "Kids",
                DataInsercao = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            nivelDAL.SalvarNivel(nivel);
            Assert.IsTrue(nivel.Id == 0, "O nível não foi salvo corretamente.");
            Assert.AreEqual("Kids", nivel.Nome, "O nome do nível não foi salvo corretamente.");
        }
    }
}
