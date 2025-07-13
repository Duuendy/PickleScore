using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.Level
{
    public partial class NivelInativado : System.Web.UI.Page
    {
        public readonly NivelDAL _nivelDAL = new NivelDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarNivelInativo();
            }
        }

        private void carregarNivelInativo()
        {
            var listaNivelInativo = _nivelDAL.ListarNiveis();
            var nivelInativo = listaNivelInativo.Where(p => p.Ativo == false).ToList();
            foreach (var p in listaNivelInativo)
            {
                System.Diagnostics.Debug.WriteLine($"Nivel => Id{p.Id}, Nome:{p.Nome}, Ativo:{p.Ativo}");
            }
            gridNivelInativo.DataSource = nivelInativo;
            gridNivelInativo.DataBind();
        }

        public void btnAtivar_Click(object sender, EventArgs e) 
        {
            bool algumSelecionado = false;

            foreach(GridViewRow row in gridNivelInativo.Rows)
            {
                CheckBox ckh = (CheckBox)row.FindControl("chkSelecionado");
                if(ckh != null && ckh.Checked) 
                {
                    int id = Convert.ToInt32(gridNivelInativo.DataKeys[row.RowIndex].Value);
                    var nivelInativo = _nivelDAL.CarregarNivel(id);

                    ViewState["NivelId"] = nivelInativo.Id;

                    nivelInativo.Ativo = true;
                    nivelInativo.DataInsercao = DateTime.Now;
                    nivelInativo.UsuarioAlteracao = 1;

                    _nivelDAL.SalvarNivel(nivelInativo);
                    algumSelecionado= true;
                }
            }

            if (algumSelecionado)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "nivelAtivado",
                        "mostrarAlerta('Usuário ativado com sucesso', 'sucesso');",
                        true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                      this,
                      GetType(),
                      "alertaSucesso",
                      "mostrarAlerta('Nenhuma categoria selecionada', 'warning');",
                      true);
            }

            ViewState["NivelId"] = null;
            carregarNivelInativo();
        }
    }
}