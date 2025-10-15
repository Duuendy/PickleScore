using PickleScore.Web.Compatilhado;
using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.User
{
    public partial class NovoUsuario : System.Web.UI.Page
    {
        private readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(Object sender, EventArgs e)
        {
            int? idUsuario = ViewState["UsuarioId"] as int?;

            if(!ValidarUsuario(out string mensagem, idUsuario))
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

            string nomeUsuario = txtNome.Text.Trim();
            string sobreNomeUsuario = txtSobrenome.Text.Trim();
            string cpfUsuario = txtCpf.Text.Trim();
            string senhaUsuario = Criptografia.CriptografarSenha(txtSenha.Text);
            string emailUsuario = txtEmail.Text.Trim();
            DateTime nascimentoUsuario = Convert.ToDateTime(txtNascimento.Text).Date;

            var novoUsario = new Models.Usuario
            {
                Id = idUsuario ?? 0,
                Nome = nomeUsuario,
                Sobrenome = sobreNomeUsuario,
                Cpf = cpfUsuario,
                Senha = senhaUsuario,
                Email = emailUsuario,
                Nascimento = nascimentoUsuario,
                Ativo = true,
                DataInsercao = DateTime.Now,
                UsuarioInsercao = 1
            };

            _usuarioDAL.CadastrarUsuario(novoUsario);

            ViewState["UsuarioId"] = null;

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "alertaSucesso",
                "mostrarAlerta('Cadastrado com Sucesso!', 'sucesso');",
                true);

            LimparCampos();

        }


        public bool ValidarUsuario(out string mensagemErro, int? idAtual)
        {
            mensagemErro = string.Empty;

            if (string.IsNullOrEmpty(txtNome.Text) ||
                string.IsNullOrEmpty(txtSobrenome.Text) ||
                string.IsNullOrEmpty(txtCpf.Text) ||
                string.IsNullOrEmpty(txtSenha.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtNascimento.Text))
            {
                mensagemErro = "Todos os campos são obrigatórios!";
                return false;
            }

            if (_usuarioDAL.UsuarioCpfDuplicado(txtCpf.Text.Trim(), idAtual))
            {
                mensagemErro = "Já existe um usuário com este CPF.";
                return false;
            }

            if (_usuarioDAL.UsuarioEmailDuplicado(txtEmail.Text.Trim(), idAtual))
            {
                mensagemErro = "Já existe um usuário com este e-mail.";
                return false;
            }

            if (!DateTime.TryParse(txtNascimento.Text, out DateTime nascimento))
            {
                mensagemErro = "Data de nascimento inválida.";
                return false;
            }
            return true;
        }

        public void LimparCampos()
        {
            txtNome.Text = "";
            txtSobrenome.Text = "";
            txtSenha.Text = "";
            txtCpf.Text = "";
            txtEmail.Text = "";
            txtNascimento.Text = "";
        }
    }
}