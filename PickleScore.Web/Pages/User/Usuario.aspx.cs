using PickleScore.Web.Compatilhado;
using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.User
{
    public partial class Usuario : System.Web.UI.Page
    {
        public readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();
        public readonly PerfilDAL _perfilDAL = new PerfilDAL();
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarUsuarios();
                carregarPerfilDropdown();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            int? idUsuario = ViewState["UsuarioId"] != null
                ? Convert.ToInt32(ViewState["UsuarioId"]) : (int?)null;

            string nomeUsuario = txtNome.Text.Trim();
            string sobreNomeUsuario = txtSobrenome.Text.Trim();
            string cpfUsuario = txtCpf.Text.Trim();
            string senhaUsuario = Criptografia.CriptografarSenha(txtSenha.Text);
            string emailUsuario = txtEmail.Text.Trim();
            DateTime nascimentoUsuario = Convert.ToDateTime(txtNascimento.Text).Date;
            int perfilUsuario = Convert.ToInt32(ddlPerfil.SelectedValue);
            

            var novoUsuario = new Models.Usuario
            {
                Id = idUsuario ?? 0,
                Nome = nomeUsuario,
                Sobrenome = sobreNomeUsuario,
                Cpf = cpfUsuario,
                Senha = senhaUsuario,
                Email = emailUsuario,
                Nascimento = nascimentoUsuario,
                PerfilId = perfilUsuario,
                Ativo = true,
                DataInsercao = DateTime.Now,
                UsuarioInsercao = 1,
                DataAlteracao = DateTime.Now,
                UsuarioAlteracao = 1
            };

            if (idUsuario.HasValue)
            {
                _usuarioDAL.AtualizarUsuario(novoUsuario);
            }
            else
            {
                _usuarioDAL.CadastrarUsuario(novoUsuario);

            }

            ViewState["UsuarioId"] = null;

            LimparCampos();
            carregarUsuarios();
        }

        public void LimparCampos()
        {
            txtNome.Text = "";
            txtSobrenome.Text = "";
            txtSenha.Text = "";
            txtCpf.Text = "";
            txtEmail.Text = "";
            txtNascimento.Text = "";
            ddlPerfil.ClearSelection();
        }

        public void gridUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                var usuario = _usuarioDAL.CarregarUsuario(id);

                txtNome.Text = usuario.Nome;
                txtSobrenome.Text = usuario.Sobrenome;
                txtEmail.Text = usuario.Email;
                txtCpf.Text = usuario.Cpf;
                txtNascimento.Text = usuario.Nascimento.ToString("yyyy-MM-dd");
                ddlPerfil.SelectedValue = usuario.PerfilId.ToString();

                ViewState["UsuarioId"] = id;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModal", "$('#modalUsuario').modal('show');", true);

            };
        }

        private void carregarPerfilDropdown()
        {
            var listarPerfis = _perfilDAL.ListarPerfis();

            ddlPerfil.DataSource = listarPerfis;
            ddlPerfil.DataTextField = "Nome";
            ddlPerfil.DataValueField = "Id";
            ddlPerfil.DataBind();

            ddlPerfil.Items.Insert(0, new ListItem("-- Selecione um perfil --", ""));
        }
        private void carregarUsuarios()
        {
            var listaUsarios = _usuarioDAL.ListarUsuarios();
            gridUsuario.DataSource = listaUsarios;
            gridUsuario.DataBind();
        }

    }
}