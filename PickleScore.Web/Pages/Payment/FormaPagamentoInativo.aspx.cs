using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.Payment
{
    public partial class FormaPagamentoInativo : System.Web.UI.Page
    {
        private readonly FormaPagamentoDAL _formaPagamentoDAL = new FormaPagamentoDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarFormaPagamentoInativo();
            }
        }

        public void btnAtivar_Click(object sender, EventArgs e)
        {
            bool algumSelecionado = false;

            foreach (GridViewRow row in gridFormaPagamentoInativo.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridFormaPagamentoInativo.DataKeys[row.RowIndex].Value);
                    var formaPagamento = _formaPagamentoDAL.CarregarFormaPagamento(id);

                    ViewState["FormaPagamentoId"] = formaPagamento.Id;

                    formaPagamento.Ativo = true;
                    formaPagamento.DataAlteracao = DateTime.Now;
                    formaPagamento.UsuarioAlteracao = 1;

                    _formaPagamentoDAL.SalvarFormaPagamento(formaPagamento);
                    algumSelecionado = true;
                }
            }

            if (algumSelecionado)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaSucesso",
                    "mostrarAlerta('Faixa Etária ativada com Sucesso!', 'sucesso');",
                    true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                      this,
                      GetType(),
                      "alertaSucesso",
                      "mostrarAlerta('Nenhum item selecionado', 'warning');",
                      true);
            }

            ViewState["FormaPagamentoId"] = null;
            carregarFormaPagamentoInativo();
        }


        private void carregarFormaPagamentoInativo()
        {
            var listarFormaPagamento = _formaPagamentoDAL.ListarFormasPagamento();
            var formaPagamentoInativo = listarFormaPagamento.Where(p => p.Ativo == false).ToList();
            foreach (var p in listarFormaPagamento)
            {
                System.Diagnostics.Debug.WriteLine($"Nivel => Nivel:{p.Id}, Nome:{p.Nome}");
            }

            gridFormaPagamentoInativo.DataSource = formaPagamentoInativo;
            gridFormaPagamentoInativo.DataBind();

        }
    }
}