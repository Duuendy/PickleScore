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
            carregarNivelInativo();
        }
    }
}