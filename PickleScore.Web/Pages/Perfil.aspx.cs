using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages
{
    public partial class Perfil : System.Web.UI.Page
    {
        public readonly PerfilDAL _perfilDAL = new PerfilDAL();
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CarregarPerfis();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            var perfil = new Models.Perfil
            {
                Nome = txtNome.Text,
                DataInsercao = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            _perfilDAL.SalvarPerfil(perfil);            
            Response.Redirect(Request.RawUrl);
        }

        //public void btnEditar_Click(object sender, EventArgs e)
        //{
        //    if (gridPerfis.SelectedRow != null)
        //    {
        //        int id = Convert.ToInt32(gridPerfis.SelectedRow.Cells[0].Text);
        //        var perfil = _perfilDAL.CarregarPerfil(id);
        //        if (perfil != null)
        //        {
        //            txtNome.Text = perfil.Nome;
        //            btnSalvar.CommandArgument = id.ToString();
        //        }
        //    }
        //}

        public void btnExcluir_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridPerfis.Rows)
            {
                CheckBox chkSelecionado = (CheckBox)row.FindControl("chkSelecionado");

                if (chkSelecionado != null && chkSelecionado.Checked)
                {
                    int perfilId = Convert.ToInt32(row.Cells[1].Text);

                    var perfilDAL = new PerfilDAL();
                    perfilDAL.DeletarPerfil(perfilId);
                }
            }
            CarregarPerfis();
        }


        private void CarregarPerfis()
        { 
            gridPerfis.DataSource = _perfilDAL.ListarPerfis();
            gridPerfis.DataBind();
        }
    }
}