using PickleScore.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.User
{
    public partial class UsuarioInativo : System.Web.UI.Page
    {
        public readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarUsuarioInativos();
            }
        }

        private void carregarUsuarioInativos()
        {
            var listaUsuarioInativo = _usuarioDAL.ListarUsuarios();
            var usuarioInativo = listaUsuarioInativo.Where(p => p.Ativo == false).ToList();
            foreach (var p in listaUsuarioInativo)
            {
                System.Diagnostics.Debug.WriteLine($"Usuario => Id:{p.Id}, Nome:{p.Nome}, Ativo:{p.Ativo}");
            }
            gridUsuariosInativos.DataSource = usuarioInativo;
            gridUsuariosInativos.DataBind();
        }

        public void btnAtivar_Click(object sender, EventArgs e)
        {
            bool algumSelecionado = false;

            foreach (GridViewRow row in gridUsuariosInativos.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridUsuariosInativos.DataKeys[row.RowIndex].Value);
                    var usuarioInativo = _usuarioDAL.CarregarUsuario(id);

                    ViewState["UsuarioId"] = usuarioInativo.Id;

                    usuarioInativo.Ativo = true;
                    usuarioInativo.DataAlteracao = DateTime.Now;
                    usuarioInativo.UsuarioAlteracao = 1;

                    _usuarioDAL.CadastrarUsuario(usuarioInativo);
                    algumSelecionado = true;
                }
            }

            if (algumSelecionado)
            {
                ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "perfilAtivado",
                        "mostrarAlerta('Usuário ativado com sucesso', 'sucesso');",
                        true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                      this,
                      GetType(),
                      "alertaSucesso",
                      "mostrarAlerta('Nenhuma categoria selecionada', 'warning');",
                      true);
            }

            ViewState["UsuarioId"] = null;
            carregarUsuarioInativos();
        }
    }
}