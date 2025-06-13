using PickleScore.Web.DAL;
using System;
using System.Linq;
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
                if (Request.QueryString["sucesso"] == "true")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Sucesso", "alert('Perfil cadastrado com sucesso!');", true);
                }

                CarregarPerfis();
            }
                
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            string nomePerfil = txtNome.Text.Trim();
            if (string.IsNullOrEmpty(nomePerfil))
            {
                lblMensagem.Text = "O nome do perfil é obrigatório.";
                return;
            }

            if (_perfilDAL.PerfilDuplicado(nomePerfil))
            {
                lblMensagem.Text = $"Perfil duplicado, {nomePerfil} já está cadastrado";
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

            txtNome.Text = string.Empty;
            CarregarPerfis();
        }

        public void btnSalvarAlteracao_Click(object sender, EventArgs e) 
        {
            string perfilAlterado = txtNomeAlteracao.Text.Trim();
            if (string.IsNullOrEmpty(perfilAlterado))
            {
                lblMensagem.Text = "O nome do perfil é obrigatório.";
                return;
            }

            if (_perfilDAL.PerfilDuplicado(perfilAlterado))
            {
                lblMensagem.Text = $"Perfil duplicado, {perfilAlterado} já está cadastrado";
                return;
            }

            if (ViewState["PerfilId"] == null)
            {
                lblMensagem.Text = "Nenhum perfil selecionado para alteração";
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
            lblMensagem.Text = "Perfil Alterado";
            blocoAlteracao.Visible = false;
            CarregarPerfis();

        }

        protected void btnEditar_Click(object sender, EventArgs e)
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
            foreach(GridViewRow row in gridPerfis.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridPerfis.DataKeys[row.RowIndex].Value);
                    var perfil = _perfilDAL.CarregarPerfil(id);

                    txtNome.Text = perfil.Nome;
                    ViewState["PerfilId"] = perfil.Id;

                    if (ViewState["PerfilId"] == null)
                    {
                        lblMensagem.Text = "Nenhum perfil selecionado para alteração";
                        return;
                    }

                    perfil.Ativo = false;
                    perfil.DataAlteracao = DateTime.Now;
                    perfil.UsuarioAlteracao = 1;

                    _perfilDAL.SalvarPerfil(perfil);
                    
                    lblMensagem.Text = "Perfil Inativado com sucesso";
                    txtNome.Text = string.Empty;
                    CarregarPerfis();
                }                
            }
        }

        private void CarregarPerfis()
        {
            var listaPerfis = _perfilDAL.ListarPerfis();
            var perfilAtivo = listaPerfis.Where(p => p.Ativo).ToList();
            foreach (var p in listaPerfis)
            {
                System.Diagnostics.Debug.WriteLine($"Perfil => Id:{p.Id}, Nome:{p.Nome}");
            }
            gridPerfis.DataSource = perfilAtivo;
            gridPerfis.DataBind();
        }
    }
}