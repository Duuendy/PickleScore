using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            string nomeNivel = txtNome.Text.Trim();
            if (string.IsNullOrEmpty(nomeNivel))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaNIvelVazio",
                    "mostrarAlerta('O nome do perfil é obrigatório.', 'erro');",
                    true);
                return;
            }

            if (_nivelDAL.NivelDuplicado(nomeNivel))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaDuplicado",
                    "mostrarAlerta('Perfil Duplicado, perfil já cadastrado', 'erro');",
                    true
                );
                return;
            }

            var nivel = new Models.Nivel
            {
                Nome = nomeNivel,
                DataInsercao = DateTime.Now,
                UsuarioInsercao = 1,
                Ativo = true,
                DataAlteracao = DateTime.Now,
                UsuarioAlteracao = 1,
            };

            _nivelDAL.SalvarNivel(nivel);

            ScriptManager.RegisterStartupScript(
               this,
               this.GetType(),
               "alertaSucesso",
               "mostrarAlerta('Perfil cadastrado com sucesso!', 'sucesso');",
               true
           );

            txtNome.Text = string.Empty;
            carregarNivel();

        }
        public void btnEditar_Click(object sender, EventArgs e)
        {

        }
        public void btnInativar_Click(object sender, EventArgs e)
        {

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