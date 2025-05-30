using Org.BouncyCastle.Crypto.Digests;
using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages
{
    public partial class Perfil : System.Web.UI.Page
    {
        public readonly PerfilDAL _perfilDAL = new PerfilDAL();
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CarregarPerfis();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            string nomePerfil = txtNome.Text.Trim();
            if (string.IsNullOrEmpty(nomePerfil))
            {
                lblMensagem.Text = "O nome do perfil é obrigatório.";
                return;
            }

            if (_perfilDAL.PerfilDuplicado(nomePerfil))
            {
                lblMensagem.Text = $"Perfil duplicado, {nomePerfil} já está cadastrado";
                return;
            }

            var perfil = new Models.Perfil
            {
                Nome = nomePerfil,
                DataInsercao = DateTime.Now,
            };

            _perfilDAL.SalvarPerfil(perfil);
            //Response.Redirect(Request.RawUrl);

            ClientScript.RegisterStartupScript(this.GetType(), "Sucesso", "<script>window.sucessoCadastro = true;</script>", false);

            txtNome.Text = string.Empty;
            CarregarPerfis();
        }

        public void btnEditar_Click(object sender, EventArgs e)
        {

        }


        private void CarregarPerfis()
        { 
            gridPerfis.DataSource = _perfilDAL.ListarPerfis();
            gridPerfis.DataBind();
        }
    }
}