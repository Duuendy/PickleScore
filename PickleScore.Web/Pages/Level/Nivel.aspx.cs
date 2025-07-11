using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.Level
{
    public partial class Nivel : System.Web.UI.Page
    {
        private readonly NivelDAL _nivelDAL = new NivelDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                carregarNivel();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            int? idNivel = ViewState["NivelId"] != null
                ? Convert.ToInt32(ViewState["NivelId"]) : (int?)null;

            string nome = !string.IsNullOrWhiteSpace(txtNomeModal.Text)
                ? txtNomeModal.Text.Trim()
                : txtNome.Text.Trim();

            if (!ValidarNivel(nome, out string mensagem, idNivel))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaValidacao",
                    $"mostrarAlerta('{mensagem}', 'warning');",
                    true);
                return;
            }

            var nivelModel = new Models.Nivel
            {
                Id = idNivel ?? 0,
                Nome = nome,
                Ativo = true
            };

            if (!idNivel.HasValue || idNivel == 0)
            {
                nivelModel.DataInsercao = DateTime.Now;
                nivelModel.UsuarioInsercao = 1;
            }
            else
            {
                nivelModel.DataAlteracao = DateTime.Now;
                nivelModel.UsuarioAlteracao = 1;
            }

            _nivelDAL.SalvarNivel(nivelModel);

            ViewState["NIvelId"] = null;
            txtNome.Text = string.Empty;
            carregarNivel();

            string tipoMensagem = idNivel.HasValue ? "Editado com sucesso!" : "Cadastrado com sucesso!";
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

            foreach(GridViewRow row in gridNivel.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridNivel.DataKeys[row.RowIndex].Value);
                    var nivel = _nivelDAL.CarregarNivel(id);

                    txtNome.Text = nivel.Nome;
                    ViewState["NivelId"] = nivel.Id;

                    nivel.Ativo = false;
                    nivel.DataAlteracao = DateTime.Now;
                    nivel.UsuarioAlteracao = 1;

                    _nivelDAL.SalvarNivel(nivel);
                    algumSelecionado = true;

                }
            }

            if (algumSelecionado)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "usuarioInativado",
                    "mostrarAlerta('Usuario Inativado com sucesso', 'sucesso');",
                    true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "usuarioInativado",
                    "mostrarAlerta('Nenhum usuário selecionado', 'warning');",
                    true);
            }

            txtNome.Text = string.Empty;
            ViewState["NivelId"] = null;
            carregarNivel();
        }

        public void abrirModalEdicao(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                var nivel = _nivelDAL.CarregarNivel(id);

                txtNomeModal.Text = nivel.Nome;

                ViewState["NivelId"] = id;

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "abrirModal",
                    "$('#modalNivel').modal('show');",
                    true);
            };
        }

        public bool ValidarNivel(string nome, out string mensagemErro, int? idAtual)
        {
            mensagemErro = string.Empty;

            if (string.IsNullOrEmpty(nome))
            {
                mensagemErro = "Nome do perfil é obrigatório!";
                return false;
            }

            if(_nivelDAL.NivelDuplicado(txtNomeModal.Text.Trim(), idAtual))
            {
                mensagemErro = "Nível já cadastrado!";
                return false;
            }

            return true;
        }

        private void carregarNivel()
        {
            var listaNiveis = _nivelDAL.ListarNiveis();
            var nivelAtivo = listaNiveis.Where(p => p.Ativo).ToList();
            foreach( var p in listaNiveis)
            {
                System.Diagnostics.Debug.WriteLine($"Nivel => Nivel:{p.Id}, Nome:{p.Nome}");
            }

            gridNivel.DataSource = nivelAtivo;
            gridNivel.DataBind();
        }
    }
}