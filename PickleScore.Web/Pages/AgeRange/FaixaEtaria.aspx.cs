using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.AgeRange
{
    public partial class FaixaEtaria : System.Web.UI.Page
    {
        public readonly FaixaEtariaDAL _faixaEtariaDAL = new FaixaEtariaDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarFaixaEtaria();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            int? idFaixaEtaria = ViewState["FaixaEtariaId"] != null
                ? Convert.ToInt32(ViewState["FaixaEtariaId"]) : (int?)null;

            string nome = !string.IsNullOrWhiteSpace(txtNomeModal.Text)
                ? txtNomeModal.Text.Trim()
                : txtNome.Text.Trim();

            if (!ValidarFaixaEtaria(nome, out string mensagem, idFaixaEtaria))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaValidacao",
                    $"mostrarAlerta('{mensagem}', 'warning');",
                    true);
                return;
            }

            var faixaEtariaModel = new Models.FaixaEtaria
            {
                Id = idFaixaEtaria ?? 0,
                Nome = nome,
                Ativo = true
            };

            if (!idFaixaEtaria.HasValue || idFaixaEtaria == 0)
            {
                faixaEtariaModel.UsuarioInsercao = 1;
                faixaEtariaModel.DataInsercao = DateTime.Now;
            }
            else
            {
                faixaEtariaModel.UsuarioAlteracao = 1;
                faixaEtariaModel.DataAlteracao = DateTime.Now;
            }

            _faixaEtariaDAL.SalvarFaixaEtaria(faixaEtariaModel);

            ViewState["FaixaEtariaId"] = null;
            txtNome.Text = string.Empty;
            carregarFaixaEtaria();

            string tipoMensagem = idFaixaEtaria.HasValue ? "Editado com sucesso!" : "Cadastrado com sucesso!";
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "alertaSucesso",
                $"mostrarAlerta('{tipoMensagem}', 'sucesso');",
                true);

        }

        public void btnInativar_Click(object sender, EventArgs e)
        {
            bool algumSelecionado = false;

            foreach(GridViewRow rows in gridFaixaEtaria.Rows)
            {
                CheckBox chk = (CheckBox)rows.FindControl("chkSelecionado");
                if (chk != null && chk.Checked)
                { 
                    int id = Convert.ToInt32(gridFaixaEtaria.DataKeys[rows.RowIndex].Value);
                    var faixaEtaria = _faixaEtariaDAL.CarregarFaixaEtaria(id);

                    txtNome.Text = faixaEtaria.Nome;
                    ViewState["FaixaEtariaId"] = faixaEtaria.Id;

                    faixaEtaria.Ativo = false;
                    faixaEtaria.DataAlteracao = DateTime.Now;
                    faixaEtaria.UsuarioAlteracao = 1;

                    _faixaEtariaDAL.SalvarFaixaEtaria(faixaEtaria);
                    algumSelecionado = true;
                }
            }

            if (algumSelecionado) 
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "usuarioInativado",
                    "mostrarAlerta('Faixa Etaria Inativado com sucesso', 'sucesso');",
                    true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "usuarioInativado",
                    "mostrarAlerta('Nenhuma Faixa Etária selecionada', 'warning');",
                    true);
            }

            txtNome.Text = string.Empty;
            ViewState["FaixaEtariaId"] = null;
            carregarFaixaEtaria();
        }

        public void abrirModalEdicao(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                var faixaEtaria = _faixaEtariaDAL.CarregarFaixaEtaria(id);

                txtNomeModal.Text = faixaEtaria.Nome;

                ViewState["FaixaEtariaId"] = id;

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "abrirModal",
                    "$('#modalFaixaEtaria').modal('show');",
                    true);
            };
        }

        private void carregarFaixaEtaria()
        {
            var listaFaixaEtaria = _faixaEtariaDAL.ListarFaixasEtarias();
            var faixaEtariaAtiva = listaFaixaEtaria.Where(p => p.Ativo).ToList();
            foreach(var p in listaFaixaEtaria)
            {
                System.Diagnostics.Debug.WriteLine($"Nivel => Nivel:{p.Id}, Nome:{p.Nome}");
            }

            gridFaixaEtaria.DataSource = faixaEtariaAtiva;
            gridFaixaEtaria.DataBind();
        }

        public bool ValidarFaixaEtaria(string nome, out string mensagemErro, int? idAtual)
        {
            mensagemErro = string.Empty;

            if (string.IsNullOrEmpty(nome))
            {
                mensagemErro = "Titulo da Faixa Etária é obrigatório";
                return false;
            }

            if(_faixaEtariaDAL.FaixaEtariaDuplicada(txtNomeModal.Text.Trim(), idAtual))
            {
                mensagemErro = "Faixa Etária já existe";
                return false;
            }
            return true;
        }
    }
}