using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.AgeRange
{
    public partial class FaixaEtaria : System.Web.UI.Page
    {
        public readonly FaixaEtariaDAL _faixaEtaria = new FaixaEtariaDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarFaixaEtaria();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        public void btnInativar_Click(object sender, EventArgs e)
        {

        }

        public void abrirModalEdicao(object sender, GridViewCommandEventArgs e)
        {

        }

        private void carregarFaixaEtaria()
        {
            var listaFaixaEtaria = _faixaEtaria.ListarFaixasEtarias();
            var faixaEtariaAtiva = listaFaixaEtaria.Where(p => p.Ativo).ToList();
            foreach(var p in listaFaixaEtaria)
            {
                System.Diagnostics.Debug.WriteLine($"Nivel => Nivel:{p.Id}, Nome:{p.Nome}");
            }

            gridFaixaEtaria.DataSource = faixaEtariaAtiva;
            gridFaixaEtaria.DataBind();
        }
    }
}