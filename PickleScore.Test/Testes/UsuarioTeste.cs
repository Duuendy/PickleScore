using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BCrypt.Net;
using BCryptNet = BCrypt.Net.BCrypt;
using PickleScore.Lib;
using PickleScore.Web.Compatilhado;

namespace PickleScore.Test
{
    [TestClass]
    public class UsuarioTeste
    {
        [TestMethod]
        public void TESTE_SALVAR_USAUARIO()
        {
            var nome = "Akira Matsuguma Haiabe";
            var senhaCripto = Criptografia.CriptografarSenha("123456");
            var cpf = "12345678902";
            var email = "akira.MH@email.com";
            var nascimento = "26/05/1988";
            var perfilId = 1;
            var dataInsercao = DateTime.Now;
            var dataAlteracao = DateTime.Now;

            var usuarioDAL = new PickleScore.Web.DAL.UsuarioDAL();
            var usuario = new PickleScore.Web.Models.Usuario
            {
                Nome = nome,
                Senha = senhaCripto,
                Cpf = cpf,
                Email = email,
                Nascimento = DateTime.Parse(nascimento),
                PerfilId = perfilId,
                DataInsercao = dataInsercao,
                DataAlteracao = dataAlteracao
            };

            Assert.IsTrue(usuario.Id == 0, "O usuário já existe.");
            Assert.AreEqual(nome, usuario.Nome, "O nome do usuário não foi salvo corretamente.");
            Assert.AreEqual(senhaCripto, usuario.Senha, "A senha do usuário não foi salva corretamente.");
            Assert.AreEqual(cpf, usuario.Cpf, "O CPF do usuário não foi salvo corretamente.");
            Assert.AreEqual(email, usuario.Email, "O email do usuário não foi salvo corretamente.");
            Assert.AreEqual(nascimento, usuario.Nascimento.ToString("dd/MM/yyyy"), "A data de nascimento do usuário não foi salva corretamente.");
            Assert.AreEqual(perfilId, usuario.PerfilId, "O perfil do usuário não foi salvo corretamente.");
            Assert.AreEqual(dataInsercao.ToString("dd/MM/yyyy"), usuario.DataInsercao.ToString("dd/MM/yyyy"), "A data de inserção do usuário não foi salva corretamente.");
            Assert.AreEqual(dataAlteracao.ToString("dd/MM/yyyy"), usuario.DataAlteracao.ToString("dd/MM/yyyy"), "A data de alteração do usuário não foi salva corretamente.");

            usuarioDAL.CadastrarUsuario(usuario);

            
        }

        [TestMethod]
        public void TESTE_CARREGAR_USUARIO()
        {
            var usuarioDAL = new PickleScore.Web.DAL.UsuarioDAL();
            var usuario = usuarioDAL.CarregarUsuario(1);
            Assert.IsNotNull(usuario, "Usuário não encontrado.");
            Assert.AreEqual(1, usuario.Id, "O ID do usuário não corresponde ao esperado.");
            Assert.IsNotNull(usuario.Nome, "O nome do usuário não foi carregado corretamente.");
            Assert.IsNotNull(usuario.Senha, "A senha do usuário não foi carregada corretamente.");
            Assert.IsNotNull(usuario.Cpf, "O CPF do usuário não foi carregado corretamente.");
            Assert.IsNotNull(usuario.Email, "O email do usuário não foi carregado corretamente.");
            Assert.IsNotNull(usuario.Nascimento, "A data de nascimento do usuário não foi carregada corretamente.");
            Assert.IsTrue(usuario.PerfilId > 0, "O perfil do usuário não foi carregado corretamente.");
        }

        [TestMethod]
        public void TESTE_ATUALIZAR_USUARIO() 
        {
            var usuarioDAL = new PickleScore.Web.DAL.UsuarioDAL();
            var usuario = usuarioDAL.CarregarUsuario(3);
            Assert.IsNotNull(usuario, "Usuário não encontrado.");
            usuario.Nome = "Hugo Calderano";
            usuarioDAL.AtualizarUsuario(usuario);
            var usuarioAtualizado = usuarioDAL.CarregarUsuario(3);
            Assert.IsNotNull(usuarioAtualizado, "Usuário atualizado não encontrado.");
            Assert.AreEqual("Hugo Calderano", usuarioAtualizado.Nome, "O nome do usuário atualizado não corresponde ao esperado.");

        }
    }
}
