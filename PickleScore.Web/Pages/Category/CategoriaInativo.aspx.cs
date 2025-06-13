using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.Category
{
    public partial class CategoriaInativo : System.Web.UI.Page
    {
        public readonly CategoriaDAL _categoriaDAL = new CategoriaDAL();
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarCategoriasInativas();
            }
        }

        private void carregarCategoriasInativas()
        {
            var listarCategoriasInativas = _categoriaDAL.ListarCategorias();
            var categoriasInativas = listarCategoriasInativas.Where(c => c.Ativo == false).ToList();
            foreach (var c in listarCategoriasInativas)
            {
                System.Diagnostics.Debug.WriteLine($"Categoria => Id: {c.Id}, Nome: {c.Nome}");
            }
            gridCategoriaInativos.DataSource = categoriasInativas;
            gridCategoriaInativos.DataBind();
        }

        public void btnAtivar_Click(object sender, EventArgs e)
        {

            foreach(GridViewRow row in gridCategoriaInativos.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridCategoriaInativos.DataKeys[row.RowIndex].Value);
                    var categoriaInativo = _categoriaDAL.CarregarCategoria(id);

                    ViewState["CategoriaId"] = categoriaInativo.Id; ;

                    categoriaInativo.Ativo = true;
                    categoriaInativo.DataAlteracao = DateTime.Now;
                    categoriaInativo.UsuarioAlteracao = 1;

                    _categoriaDAL.SalvarCategoria(categoriaInativo);

                    lblMensagemInativos.Text = "Categoria ativada com sucesso";
                    carregarCategoriasInativas();
                }
            }
        }
    }
}