using Microsoft.VisualStudio.TestTools.UnitTesting;
using PickleScore.Web.DAL;
using PickleScore.Web.Models;
using System;
using System.Collections.Generic;

namespace PickleScore.Test
{
    [TestClass]
    public class PerfilTeste
    {
        private PerfilDAL _perfilDAL;
        private Perfil _perfilInserido;

        [TestInitialize]
        public void Setup()
        {
            _perfilDAL = new PerfilDAL();
            _perfilInserido = new Perfil
            {
                Nome = "Teste",
                DataInsercao = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            _perfilDAL.SalvarPerfil(_perfilInserido);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_perfilInserido != null && _perfilInserido.Id > 0)
            {
                _perfilDAL.DeletarPerfil(_perfilInserido.Id);
            }
        }



        [TestMethod]
        public void CADASTRAR_PERFIL()
        {
            var nome = "UsuarioTeste";
            var dataInsercao = DateTime.Now;
            var dataAlteracao = DateTime.Now;

            var perfilDAL = new PickleScore.Web.DAL.PerfilDAL();
            var perfil = new PickleScore.Web.Models.Perfil
            {
                Nome = nome,
                DataInsercao = dataInsercao,
                DataAlteracao = dataAlteracao
            };

            Assert.IsTrue(perfil.Id == 0, "O perfil já existe.");
            Assert.AreEqual(nome, perfil.Nome, "O nome do perfil não foi salvo corretamente.");
            perfilDAL.SalvarPerfil(perfil);
        }

        [TestMethod]
        public void CARREGAR_PERFIL()
        {
           var perfil = _perfilDAL.CarregarPerfil(_perfilInserido.Id);
           Assert.IsNotNull(perfil, "O perfil não foi carregado corretamente.");
           Assert.AreEqual(_perfilInserido.Nome, perfil.Nome, "O nome não corresponde.");
        }

        [TestMethod]
        public void ATUALIZAR_PERFIL()
        {
            _perfilInserido.Nome = "Perfil Atualizado";
            _perfilInserido.DataAlteracao = DateTime.Now;

            _perfilDAL.SalvarPerfil(_perfilInserido);
            var atualizado = _perfilDAL.CarregarPerfil(_perfilInserido.Id);

            Assert.AreEqual("Perfil Atualizado", atualizado.Nome, "Nome não foi atualizado.");
        }

        [TestMethod]

        public void EXCLUIR_PERFIL()
        {
            var perfilDAL = new PickleScore.Web.DAL.PerfilDAL();
            var perfil = perfilDAL.CarregarPerfil(4);
            Assert.IsNotNull(perfil, "O perfil não foi carregado corretamente.");
            perfilDAL.DeletarPerfil(perfil.Id);
            var perfilExcluido = perfilDAL.CarregarPerfil(4);
            Assert.IsNull(perfilExcluido, "O perfil não foi excluído corretamente.");
        }

        [TestMethod]
        public void LISTAR_TODOS_PERFIS()
        {
            var lista = _perfilDAL.ListarPerfis();
            Assert.IsNotNull(lista, "A lista de perfis não foi carregada corretamente.");
        }
    }
}
