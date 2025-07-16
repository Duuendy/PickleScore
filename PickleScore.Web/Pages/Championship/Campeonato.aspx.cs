using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.Championship
{
    public partial class Campeonato : System.Web.UI.Page
    {
        private readonly CampeonatoDAL _campeonato = new CampeonatoDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                carregarCampeonato();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        public void btnInativar_Click(Object sender, EventArgs e)
        {

        }

        public void abrirModalEdicao(object sender, GridViewCommandEventArgs e)
        {

        }

        private void carregarCampeonato()
        {
            var listarCampeonatos = _campeonato.ListarCampeonatos();
            var campeonatosAtivos = listarCampeonatos.Where(p => p.Ativo).ToList();
            foreach(var p in listarCampeonatos)
            {
                System.Diagnostics.Debug.WriteLine($"Nivel => Nivel:{p.Id}, Nome:{p.Nome}, Ativo:{p.Ativo}");
            }

            gridCampeonato.DataSource = campeonatosAtivos;
            gridCampeonato.DataBind();
        }
    }

    
}