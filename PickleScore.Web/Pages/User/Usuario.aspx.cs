using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.User
{
    public partial class Usuario : System.Web.UI.Page
    {
        public readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarUsuarios();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            //string nomeUsuario = txtNome.Text.Trim();
            //if (string.IsNullOrEmpty(nomeUsuario))
            //{
            //    lblMensagem.Text = "O nome do Usuário é obrigatório";
            //    return;
            //}

            //if (_usuarioDAL.UsuarioDuplicado(nomeUsuario))
            //{
            //    lblMensagem.Text = $"Usuário duplicado, {nomeUsuario} já está cadastrado";
            //}

            //var usuario = new Models.Usuario
            //{
            //    Nome = nomeUsuario,
            //    Cpf = 
            //}
        }
        private void carregarUsuarios()
        {
            var listaUsarios = _usuarioDAL.ListarUsuarios();
            gridUsuario.DataSource = listaUsarios;
            gridUsuario.DataBind();
        }

    }
}