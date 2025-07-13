using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.AgeRange
{
    public partial class FaixaEtariaInativa : System.Web.UI.Page
    {
        private readonly FaixaEtariaDAL _faixaEtariaDAL= new FaixaEtariaDAL();
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarFaixaEtariaInativado();

            }
        }

        public void btnAtivar_Click(object sender, EventArgs e)
        {
            var algumSelecionado = false;

            foreach(GridViewRow rows in gridFaixaEtariaInativo.Rows)
            {
                CheckBox chk = (CheckBox)rows.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridFaixaEtariaInativo.DataKeys[rows.RowIndex].Value);
                    var faixaEtariaInativo = _faixaEtariaDAL.CarregarFaixaEtaria(id);

                    ViewState["FaixaEtariaId"] = faixaEtariaInativo.Id;

                    faixaEtariaInativo.Ativo = true;
                    faixaEtariaInativo.DataAlteracao = DateTime.Now;
                    faixaEtariaInativo.UsuarioAlteracao = 1;

                    _faixaEtariaDAL.SalvarFaixaEtaria(faixaEtariaInativo);
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

            ViewState["CategoriaId"] = null;
            carregarFaixaEtariaInativado();
        }

        private void carregarFaixaEtariaInativado()
        {
            var listarFaixaEtariaInativado = _faixaEtariaDAL.ListarFaixasEtarias();
            var FaixaEtariaInativo = listarFaixaEtariaInativado.Where(p => p.Ativo == false).ToList();
            foreach(var p in listarFaixaEtariaInativado)
            {
                System.Diagnostics.Debug.WriteLine($"Faixa Etaria => Id{p.Id}, Nome:{p.Nome}, Ativo:{p.Ativo}");
            }

            gridFaixaEtariaInativo.DataSource = FaixaEtariaInativo;
            gridFaixaEtariaInativo.DataBind();
        }
    }
}