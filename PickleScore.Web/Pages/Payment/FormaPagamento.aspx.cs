using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.Payment
{
    public partial class FormaPagamento : System.Web.UI.Page
    {
        private readonly FormaPagamentoDAL _formaPagamentoDAL = new FormaPagamentoDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarFormaPagamento();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            int? idFormaPagamento = ViewState["FormaPagamentoId"] != null
                ? Convert.ToInt32(ViewState["FormaPagamentoId"]) : (int?)null;

            string nome = !string.IsNullOrWhiteSpace(txtNomeModal.Text)
                ? txtNomeModal.Text.Trim()
                : txtNome.Text.Trim();

            if (!validarFormaPagamento(nome, out string mensagem, idFormaPagamento))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaValidacao",
                    $"mostrarAlerta('{mensagem}', 'warning');",
                    true);
                return;
            }

            var formaPagamentoModel = new Models.FormaPagamento
            {
                Id = idFormaPagamento ?? 0,
                Nome = nome,
                Ativo = true
            };

            if (!idFormaPagamento.HasValue || idFormaPagamento == 0)
            {
                formaPagamentoModel.UsuarioInsercao = 1;
                formaPagamentoModel.DataInsercao = DateTime.Now;
            }
            else
            {
                formaPagamentoModel.UsuarioAlteracao = 1;
                formaPagamentoModel.DataAlteracao = DateTime.Now;
            }

            _formaPagamentoDAL.SalvarFormaPagamento(formaPagamentoModel);

            ViewState["FormaPagamentoId"] = null;
            txtNome.Text = string.Empty;
            carregarFormaPagamento();

            string tipoMensagem = idFormaPagamento.HasValue ? "Editado com sucesso!" : "Cadastrado com sucesso!";
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

            foreach(GridViewRow row in gridFormaPagamento.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridFormaPagamento.DataKeys[row.RowIndex].Value);
                    var formaPagamento = _formaPagamentoDAL.CarregarFormaPagamento(id);

                    txtNome.Text = formaPagamento.Nome;
                    ViewState["FormaPagamentoId"] = formaPagamento.Id;

                    formaPagamento.Ativo = false;
                    formaPagamento.UsuarioAlteracao = 1;
                    formaPagamento.DataAlteracao = DateTime.Now;

                    _formaPagamentoDAL.SalvarFormaPagamento(formaPagamento);
                    algumSelecionado = true;
                }
            }

            if (algumSelecionado)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "usuarioInativado",
                    "mostrarAlerta('Forma Pagamento Inativado com sucesso', 'sucesso');",
                    true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                   this,
                   GetType(),
                   "usuarioInativado",
                   "mostrarAlerta('Nenhuma Forma Pagamento selecionada', 'warning');",
                   true);
            }

            txtNome.Text = string.Empty;
            ViewState["FormaPagamentoId"] = null;
            carregarFormaPagamento();
        }

        public void abrirModalEdicao(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                var formaPagamento = _formaPagamentoDAL.CarregarFormaPagamento(id);

                txtNomeModal.Text = formaPagamento.Nome;

                ViewState["FormaPagamentoId"] = id;

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "abrirModal",
                    "$('#modalFormaPagamento').modal('show');",
                    true);
            };
        }

        private void carregarFormaPagamento()
        {
            var listaFormaPagamento = _formaPagamentoDAL.ListarFormasPagamento();
            var formaPagamentoAtiva = listaFormaPagamento.Where(p => p.Ativo).ToList();
            foreach(var p in listaFormaPagamento)
            {
                System.Diagnostics.Debug.WriteLine($"Nivel => Nivel:{p.Id}, Nome:{p.Nome}, Ativo:{p.Ativo}");
            }

            gridFormaPagamento.DataSource = formaPagamentoAtiva;
            gridFormaPagamento.DataBind();
        }

        private bool validarFormaPagamento(string nome, out string mensagemErro, int? idAtual)
        {
            mensagemErro = string.Empty;
            if(string.IsNullOrEmpty(nome))
            {
                mensagemErro = "Campo obrigatorio!!!";
                return false;
            }

            if(_formaPagamentoDAL.FormaPagamentoDuplicado(nome, idAtual))
            {
                mensagemErro = "Forma de Pagamento já existe";
                return false;
            }

            return true;
        }
    }
}