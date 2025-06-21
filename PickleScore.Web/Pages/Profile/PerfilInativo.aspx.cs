using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.Profile
{
    public partial class PerfilInativo : System.Web.UI.Page
    {
        public readonly PerfilDAL _perfilDAL = new PerfilDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               carregarPerfisInativos();
            }
        }

        private void carregarPerfisInativos()
        {
            var listarPerfisInativos = _perfilDAL.ListarPerfis();
            var perfilInativo = listarPerfisInativos.Where(p => p.Ativo == false).ToList();
            foreach (var p in listarPerfisInativos)
            {
                System.Diagnostics.Debug.WriteLine($"Perfil => Id:{p.Id}, Nome:{p.Nome}");
            }
            gridPerfisInativos.DataSource = perfilInativo;
            gridPerfisInativos.DataBind();
        }

        public void btnAtivar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridPerfisInativos.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridPerfisInativos.DataKeys[row.RowIndex].Value);
                    var perfilInativo = _perfilDAL.CarregarPerfil(id);

                    ViewState["PerfilId"] = perfilInativo.Id;

                    perfilInativo.Ativo = true;
                    perfilInativo.DataAlteracao = DateTime.Now;
                    perfilInativo.UsuarioAlteracao = 1;

                    _perfilDAL.SalvarPerfil(perfilInativo);

                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "perfilAtivado",
                        "mostrarAlerta('Perfil ativado com sucesso', 'sucesso');",
                        true);

                    carregarPerfisInativos();

                }
            }
            
        }
    }
}