using PickleScore.Web.DAL;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.Category
{
    public partial class Categoria : System.Web.UI.Page
    {
        private readonly CategoriaDAL _categoriaDAL= new PickleScore.Web.DAL.CategoriaDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarCategoria();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            string nomeCategoria = txtNome.Text.Trim();
            if (string.IsNullOrEmpty(nomeCategoria))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaNomeVazio",
                    "mostrarAlerta('O nome da categoria é obrigatória.', 'erro');",
                    true);
                return;
            }

            if (_categoriaDAL.CategoriaDuplicada(nomeCategoria))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "AlertaDuplicado",
                    "mostrarAlerta('Categoria duplicada', 'warning');",
                    true);
                return;
            }

            var categoria = new Models.Categoria
            {
                Nome = nomeCategoria,
                DataInsercao = DateTime.Now,
                UsuarioInsercao = 1,
                Ativo = true,
                DataAlteracao = DateTime.Now,
                UsuarioAlteracao = 1,
            };

            _categoriaDAL.SalvarCategoria(categoria);

            txtNome.Text = string.Empty;

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "alertaSucesso",
                "mostrarAlerta('Cadatrada com sucesso!', 'sucesso')",
                true);
            CarregarCategoria();
        }

        public void btnSalvarAlteracao_Click(object sender, EventArgs e)
        {
            string categoriaAlteracao = txtNomeAlteracao.Text.Trim();
            if(string.IsNullOrEmpty(categoriaAlteracao))
            {
                lblMensagem.Text = "O nome da categoria é obrigatório.";
                return;
            }

            if(_categoriaDAL.CategoriaDuplicada(categoriaAlteracao))
            {
                lblMensagem.Text = "Categoria já Existe.";
                return;
            }

            if (ViewState["CategoriaId"] == null)
            {
                lblMensagem.Text = "Nenhuma categoria selecionada para alteração.";
                return;
            }

            int categoriaId = Convert.ToInt32(ViewState["CategoriaId"]);
            var categoriaAtual = _categoriaDAL.CarregarCategoria(categoriaId);

            var novaCategoria = new Models.Categoria
            {
                Id = categoriaId,
                Nome = categoriaAlteracao,
                Ativo = categoriaAtual.Ativo,
                DataAlteracao = DateTime.Now,
                UsuarioAlteracao = 1
            };

            _categoriaDAL.SalvarCategoria(novaCategoria);
            ViewState["CategoriaId"] = null;
            txtNome.Text = string.Empty;
            lblMensagem.Text = "Categoria atualizada com sucesso!";
            blocoAlteracao.Visible = false;
            CarregarCategoria();
        }

        public void btnEditar_Click(object sender, EventArgs e)
        {
            foreach(GridViewRow row in gridCategorias.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridCategorias.DataKeys[row.RowIndex].Value);
                    var categoria = _categoriaDAL.CarregarCategoria(id);

                    txtNome.Text = categoria.Nome;
                    ViewState["CategoriaId"] = categoria.Id;

                    txtNome.Text = string.Empty;
                    blocoAlteracao.Visible = true;
                    break;
                }
            }
        }

        public void btnInativar_Click(object sender, EventArgs e)
        {
            foreach(GridViewRow row in gridCategorias.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridCategorias.DataKeys[row.RowIndex].Value);
                    var categoria = _categoriaDAL.CarregarCategoria(id);

                    txtNome.Text = categoria.Nome;
                    ViewState["CategoriaId"] = categoria.Id;

                    if (ViewState["CategoriaId"] == null)
                    {
                        lblMensagem.Text = "Nenhuma categorua selecionada para alteração";
                    }

                    categoria.Ativo = false;
                    categoria.DataAlteracao = DateTime.Now;
                    categoria.UsuarioAlteracao = 1;

                    _categoriaDAL.SalvarCategoria(categoria);

                    lblMensagem.Text = "Categoria Inativado com sucesso";
                    txtNome.Text = string.Empty;
                    CarregarCategoria();
                }
            }
        }


        private void CarregarCategoria()
        {
            var listaCategoria = _categoriaDAL.ListarCategorias();
            var categoriaAtivo = listaCategoria.Where(p => p.Ativo).ToList();
            foreach(var p in listaCategoria)
            {
                System.Diagnostics.Debug.WriteLine($"Perfil => Id:{p.Id}, Nome:{p.Nome}");
            }
            gridCategorias.DataSource = categoriaAtivo;
            gridCategorias.DataBind();
        }
    }
}