using Mysqlx;
using PickleScore.Web.DAL;
using PickleScore.Web.Models;
using PickleScore.Web.Pages.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.Login
{
    public partial class Login : System.Web.UI.Page
    {
        public readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAcessar_Click(object sender, EventArgs e)
        {
            string nomeLogin = txtUsuario.Text.Trim().ToLowerInvariant();
            string senhaLogin = txtSenha.Text.Trim();

            if (!ValidarCampos(out string mensagem, out var email, out var senha))
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "alertaValidacao",
                        $"mostrarAlerta('{mensagem}', 'warning');",
                        true
                    );
                return;
            }

            var usuario = _usuarioDAL.ObterEmail(nomeLogin);

            if(usuario == null)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "alertaValidacao",
                        $"mostrarAlerta('{mensagem}', 'warning');",
                        true
                    );
                return;
            }

            if(usuario.Ativo == false)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "alertaValidacao",
                        $"mostrarAlerta('{mensagem}', 'warning');",
                        true
                    );
                return;
            }

            if(string.IsNullOrEmpty(usuario.Senha) || !BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "alertaValidacao",
                        $"mostrarAlerta('{mensagem}', 'warning');",
                        true
                    );
                return;
            }
            
            Session["UsuarioId"] = usuario.Id;
            Session["UsuarioNome"] = usuario.Nome;
            Session["UsuarioSenha"] = usuario.Senha;

            Response.Redirect("~/Pages/User/Usuario.aspx", endResponse: false);

        }

        public bool ValidarCampos(out string mensagemErro, out string emailNormalizado, out string senhaDigitada)
        {
            mensagemErro = string.Empty;
            emailNormalizado = txtUsuario.Text?.Trim().ToLowerInvariant();
            senhaDigitada = txtSenha.Text;

            //campos em branco
            if (string.IsNullOrEmpty(emailNormalizado) || 
                string.IsNullOrEmpty(senhaDigitada))
            {
                mensagemErro = "Todos os campos são obrigatórios";
                return false;
            }

            //login ou senha errada
            if (!Regex.IsMatch(emailNormalizado, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mensagemErro = "E-mail inválido";
                return false;
            }

            return true;   
        }
        

    }
}