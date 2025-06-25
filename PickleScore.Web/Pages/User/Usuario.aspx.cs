using PickleScore.Web.Compatilhado;
using PickleScore.Web.DAL;
using PickleScore.Web.Models;
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

            if (!idUsuario.HasValue ||idUsuario.Value == 0)
            {
                if (!ValidarUsuario(out string mensagem, idUsuario))
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

                _usuarioDAL.CadastrarUsuario(novoUsuario);
            }
            else
            {
                string nomeUsuarioEditado = txtNome.Text.Trim();
                string sobreNomeUsuarioEditado = txtSobrenome.Text.Trim();
                string cpfUsuarioEditado = txtCpf.Text.Trim();
                string senhaUsuarioEditado = Criptografia.CriptografarSenha(txtSenha.Text);
                string emailUsuarioEditado = txtEmail.Text.Trim();
                DateTime nascimentoUsuarioEditado = Convert.ToDateTime(txtNascimento.Text).Date;
                int perfilUsuarioEditado = Convert.ToInt32(ddlPerfil.SelectedValue);

                var perfilEditado = new Models.Usuario
                {
                    Id = idUsuario ?? 0,
                    Nome = nomeUsuarioEditado,
                    Sobrenome = sobreNomeUsuarioEditado,
                    Cpf = cpfUsuarioEditado,
                    Senha = senhaUsuarioEditado,
                    Email = emailUsuarioEditado,
                    Nascimento = nascimentoUsuarioEditado,
                    PerfilId = perfilUsuarioEditado,
                    Ativo = true,
                    DataInsercao = DateTime.Now,
                    UsuarioInsercao = 1,
                    DataAlteracao = DateTime.Now,
                    UsuarioAlteracao = 1
                };

                _usuarioDAL.AtualizarUsuario(perfilEditado);

            }


            ViewState["UsuarioId"] = null;

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "alertaSucesso",
                "mostrarAlerta('Cadastrado com Sucesso!', 'sucesso');",
                true);

            LimparCampos();
            carregarUsuarios();
        }

        

        public void btnInativar_Click(Object sender, EventArgs e)
        {
            bool algumSelecionado = false;

            foreach(GridViewRow row in gridUsuario.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridUsuario.DataKeys[row.RowIndex].Value);
                    var usuario = _usuarioDAL.CarregarUsuario(id);

                    txtNome.Text = usuario.Nome;
                    ViewState["UsuarioId"] = usuario.Id;

                    usuario.Ativo = false;
                    usuario.DataAlteracao = DateTime.Now;
                    usuario.UsuarioAlteracao = 1;

                    _usuarioDAL.CadastrarUsuario(usuario);
                    algumSelecionado = true;
                }
            }

            if (algumSelecionado)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "usuarioInativado",
                    "mostrarAlerta('Usuario Inativado com sucesso', 'sucesso');",
                    true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "usuarioInativado",
                    "mostrarAlerta('Nenhum usuário selecionado', 'warning');",
                    true);
            }

            txtNome.Text = string.Empty;
            ViewState["UsuarioId"] = null;
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

        public bool ValidarUsuario(out string mensagemErro, int? idAtual)
        {
            mensagemErro = string.Empty;

            if(string.IsNullOrEmpty(txtNome.Text) ||
                string.IsNullOrEmpty(txtSobrenome.Text) ||
                string.IsNullOrEmpty(txtCpf.Text) ||
                string.IsNullOrEmpty(txtSenha.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtNascimento.Text) ||
                ddlPerfil.SelectedIndex == 0)
            {
                mensagemErro = "Todos os campos são obrigatórios!";
                return false;
            }

            if (_usuarioDAL.UsuarioCpfDuplicado(txtCpf.Text.Trim(), idAtual))
            {
                mensagemErro = "Já existe um usuário com este CPF.";
                return false; 
            }

            if(_usuarioDAL.UsuarioEmailDuplicado(txtEmail.Text.Trim(), idAtual))
            {
                mensagemErro = "Já existe um usuário com este e-mail.";
                return false;
            }

            if(!DateTime.TryParse(txtNascimento.Text, out DateTime nascimento))
            {
                mensagemErro = "Data de nascimento inválida.";
                return false;
            }
            return true;
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
            var usuarioAtivo = listaUsarios.Where(p => p.Ativo).ToList();
            foreach(var p in listaUsarios)
            {
                System.Diagnostics.Debug.WriteLine($"Usuario => Id:{p.Id}, Nome:{p.Nome}");
            }
            gridUsuario.DataSource = usuarioAtivo;
            gridUsuario.DataBind();
        }

    }
}