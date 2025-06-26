using PickleScore.Web.DAL;
using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.Profile
{
    public partial class PerfilPages : System.Web.UI.Page
    {
        public readonly PerfilDAL _perfilDAL = new PerfilDAL();
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                carregarPerfis();
            }
                
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            string nomePerfil = txtNome.Text.Trim();
            if (string.IsNullOrEmpty(nomePerfil))
            {
                ScriptManager.RegisterStartupScript(
                    this, 
                    GetType(), 
                    "alertaNomeVazio",
                    "mostrarAlerta('O nome do perfil é obrigatório.', 'erro');",
                    true);
                return;
            }

            if (_perfilDAL.PerfilDuplicado(nomePerfil))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaDuplicado",
                    "mostrarAlerta('Perfil Duplicado, perfil já cadastrado', 'erro');",
                    true
                );
                return;
            }

            var perfil = new Models.Perfil
            {
                Nome = nomePerfil,
                DataInsercao = DateTime.Now,
                UsuarioInsercao = 1,
                Ativo = true,
                DataAlteracao = DateTime.Now,
                UsuarioAlteracao = 1
            };

            _perfilDAL.SalvarPerfil(perfil);

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "alertaSucesso",
                "mostrarAlerta('Perfil cadastrado com sucesso!', 'sucesso');",
                true
            );

            txtNome.Text = string.Empty;
            carregarPerfis();
        }

        public void btnSalvarAlteracao_Click(object sender, EventArgs e) 
        {
            string perfilAlterado = txtNomeAlteracao.Text.Trim();
            if (string.IsNullOrEmpty(perfilAlterado))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaNomeVazio",
                    "mostrarAlerta('O nome do perfil é obrigatório.', 'erro');",
                    true);
                return;
            }

            if (_perfilDAL.PerfilDuplicado(perfilAlterado))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaDuplicado",
                    "mostrarAlerta('Perfil Duplicado, perfil já cadastrado', 'erro');",
                    true
                );
                return;
            }

            if (ViewState["PerfilId"] == null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaNaoSelecionado",
                    "mostrarAlerta('Obrigatório selecionar um perfil', 'warning');",
                    true
                );
                return;

            }

            int id = Convert.ToInt32(ViewState["PerfilId"]);
            var perfilAtual = _perfilDAL.CarregarPerfil(id);

            var novoPerfil = new Models.Perfil
            {
                Id = id,
                Nome = perfilAlterado,
                Ativo = perfilAtual.Ativo,
                DataAlteracao = DateTime.Now,
                UsuarioAlteracao = 1
            };

            _perfilDAL.SalvarPerfil(novoPerfil);
            ViewState["PerfilId"] = null;
            txtNomeAlteracao.Text = string.Empty;
            //lblMensagem.Text = "Perfil Alterado";    
            blocoAlteracao.Visible = false;
            carregarPerfis();
            
        }

        public void btnEditar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridPerfis.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridPerfis.DataKeys[row.RowIndex].Value);
                    var perfil = _perfilDAL.CarregarPerfil(id);

                    txtNome.Text = perfil.Nome;
                    ViewState["PerfilId"] = perfil.Id;

                    txtNome.Text = string.Empty;
                    blocoAlteracao.Visible = true;
                    break;
                }
            }
        }

        public void btnInativar_Click(object sender, EventArgs e)
        {
            bool algumSelecionado = false;

            foreach (GridViewRow row in gridPerfis.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridPerfis.DataKeys[row.RowIndex].Value);
                    var perfil = _perfilDAL.CarregarPerfil(id);

                    txtNome.Text = perfil.Nome;
                    ViewState["PerfilId"] = perfil.Id;

                    perfil.Ativo = false;
                    perfil.DataAlteracao = DateTime.Now;
                    perfil.UsuarioAlteracao = 1;

                    _perfilDAL.SalvarPerfil(perfil);
                    algumSelecionado = true;
                }                
            }

            if (algumSelecionado)
            {
                ScriptManager.RegisterStartupScript(
                   this,
                   GetType(),
                   "perfilInativado",
                   "mostrarAlerta('Perfil Inativado com sucesso', 'sucesso');",
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
            ViewState["PerfilId"] = null;
            carregarPerfis();
        }

        private void carregarPerfis()
        {
            var listaUsuario = _perfilDAL.ListarPerfis();
            var usuarioAtivo = listaUsuario.Where(p => p.Ativo).ToList();
            foreach (var p in listaUsuario)
            {
                System.Diagnostics.Debug.WriteLine($"Perfil => Id:{p.Id}, Nome:{p.Nome}");
            }
            gridPerfis.DataSource = usuarioAtivo;
            gridPerfis.DataBind();
        }
    }
}