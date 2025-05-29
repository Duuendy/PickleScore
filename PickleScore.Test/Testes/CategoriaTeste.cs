using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PickleScore.Test
{
    [TestClass]
    public class CategoriaTeste
    {
        [TestMethod]
        public void TESTE_CADASTRAR_CATEGORIA()
        {
            var categoriaDAL = new PickleScore.Web.DAL.CategoriaDAL();
            var categoria = new PickleScore.Web.Models.Categoria
            {
                Nome = "Kids",
                DataInsercao = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            categoriaDAL.SalvarCategoria(categoria);
            Assert.IsTrue(categoria.Id == 0, "A categoria não foi salva corretamente.");
            Assert.AreEqual("Kids", categoria.Nome, "O nome da categoria não foi salvo corretamente.");
        }

        [TestMethod]
        public void TESTE_CARREGAR_CATEGORIA()
        {
            var categoriaDAL = new PickleScore.Web.DAL.CategoriaDAL();
            var categoria = categoriaDAL.CarregarCategoria(1);
            Assert.IsNotNull(categoria, "Categoria não encontrada.");
            Assert.AreEqual(1, categoria.Id, "O ID da categoria não corresponde ao esperado.");
            Assert.IsNotNull(categoria.Nome, "O nome da categoria não foi carregado corretamente.");
        }

        [TestMethod]

        public void TESTE_ATUALIZAR_CATEGORIA()
        {
            var categoriaDAL = new PickleScore.Web.DAL.CategoriaDAL();
            var categoria = categoriaDAL.CarregarCategoria(5);
            Assert.IsNotNull(categoria, "Categoria não encontrada.");
            categoria.Nome = "Dupla Mista";
            categoria.DataAlteracao = DateTime.Now;
            categoriaDAL.AtualizarCategoria(categoria);
            var categoriaAtualizada = categoriaDAL.CarregarCategoria(5);
            Assert.AreEqual("Dupla Mista", categoriaAtualizada.Nome, "O nome da categoria não foi atualizado corretamente.");
        }
    }
}
