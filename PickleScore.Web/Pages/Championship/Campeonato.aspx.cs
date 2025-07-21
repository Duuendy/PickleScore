using Microsoft.Ajax.Utilities;
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
        private readonly CampeonatoDAL _campeonatoDAL = new CampeonatoDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                carregarCampeonato();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            int? idCampeonato = ViewState["CampeonatoId"] != null
                ? Convert.ToInt32(ViewState["CampeonatoId"]) : (int?)null;

            if(!idCampeonato.HasValue || idCampeonato == 0)
            {
                if (!validarCampeonato(out string mensagem, idCampeonato))
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "alertaValidacao",
                        $"mostrarAlerta('{mensagem}', 'warning');",
                        true);
                    return;
                }

                string nome = txtNome.Text.Trim();
                string local = txtLocal.Text.Trim();
                DateTime dataInicio = Convert.ToDateTime(txtDataInicio.Text).Date;
                DateTime dataFim = Convert.ToDateTime(txtDataFim.Text).Date;

                var novoCampeonato = new Models.Campeonato
                {
                    Id = idCampeonato ?? 0,
                    Nome = nome,
                    Local = local,
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    Ativo = true,
                    UsuarioInsercao = 1,
                    DataInsercao = DateTime.Now,
                };

                _campeonatoDAL.CadastrarCampeonato(novoCampeonato);

            }
            else
            {
                string nomeEditado = txtNome.Text.Trim();
                string localEditado = txtLocal.Text.Trim();
                DateTime dataInicioEditado = Convert.ToDateTime(txtDataInicio.Text).Date;
                DateTime dataFimEditado = Convert.ToDateTime(txtDataFim.Text).Date;

                var campeonatoEditado = new Models.Campeonato
                {
                    Id = idCampeonato ?? 0,
                    Nome = nomeEditado,
                    Local = localEditado,
                    DataInicio = dataInicioEditado,
                    DataFim = dataFimEditado,
                    Ativo = true,
                    UsuarioAlteracao = 1,
                    DataAlteracao = DateTime.Now,
                };

                campeonatoEditado.Id = idCampeonato.Value;
                _campeonatoDAL.CadastrarCampeonato(campeonatoEditado);
            }

            ViewState["CampeonatoId"] = null;

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "alertaSucesso",
                "mostrarAlerta('Cadastrado com Sucesso!', 'sucesso');",
                true);

            LimparCampos();
            carregarCampeonato();
        }

        public void abrirModal(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                var campeonato = _campeonatoDAL.CarregarCampeonato(id);

                txtNome.Text = campeonato.Nome;
                txtLocal.Text = campeonato.Local;
                txtDataInicio.Text = campeonato.DataInicio.ToString("dd-MM-yyyy");
                txtDataFim.Text = campeonato.DataFim.ToString("dd-MM-yyyy");

                ViewState["CampeonatoId"] = id;

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "abrirModal",
                    "$('#modalCampeonato').modal('show');",
                    true
                );
            };
        }

        private void carregarCampeonato()
        {
            var listarCampeonatos = _campeonatoDAL.ListarCampeonatos();
            var campeonatosAtivos = listarCampeonatos.Where(p => p.Ativo).ToList();
            foreach(var p in listarCampeonatos)
            {
                System.Diagnostics.Debug.WriteLine($"Nivel => Nivel:{p.Id}, Nome:{p.Nome}, Ativo:{p.Ativo}");
            }

            gridCampeonato.DataSource = campeonatosAtivos;
            gridCampeonato.DataBind();
        }

        private bool validarCampeonato(out string mensagemErro, int? idAtual)
        {
            mensagemErro = string.Empty;

            string nome = txtNome.Text.Trim();
            string local = txtLocal.Text.Trim();
            string dataInicio = txtDataInicio.Text.Trim();
            string dataFim = txtDataFim.Text.Trim();

            if (string.IsNullOrWhiteSpace(nome))
            {
                mensagemErro = "O campo Nome é obrigatório.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(local))
            {
                mensagemErro = "O campo Local é obrigatório.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dataInicio))
            {
                mensagemErro = "O campo Data Início é obrigatório.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dataFim))
            {
                mensagemErro = "O campo Data Fim é obrigatório.";
                return false;
            }

            return true;
        }

        public void LimparCampos()
        {
            txtNome.Text = "";
            txtLocal.Text = "";
            txtDataInicio.Text = "";
            txtDataFim.Text = "";
        }
    }
}