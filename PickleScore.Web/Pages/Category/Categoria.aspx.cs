﻿using PickleScore.Web.DAL;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickleScore.Web.Pages.Category
{
    public partial class Categoria : System.Web.UI.Page
    {
        private readonly CategoriaDAL _categoriaDAL= new PickleScore.Web.DAL.CategoriaDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarCategoria();
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            string nomeCategoria = txtNome.Text.Trim();
            if (string.IsNullOrEmpty(nomeCategoria))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaNomeVazio",
                    "mostrarAlerta('O nome da categoria é obrigatória.', 'erro');",
                    true);
                return;
            }

            if (_categoriaDAL.CategoriaDuplicada(nomeCategoria))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "AlertaDuplicado",
                    "mostrarAlerta('Categoria duplicada', 'warning');",
                    true);
                return;
            }

            var categoria = new Models.Categoria
            {
                Nome = nomeCategoria,
                DataInsercao = DateTime.Now,
                UsuarioInsercao = 1,
                Ativo = true,
                DataAlteracao = DateTime.Now,
                UsuarioAlteracao = 1,
            };

            _categoriaDAL.SalvarCategoria(categoria);

            txtNome.Text = string.Empty;

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "alertaSucesso",
                "mostrarAlerta('Cadatrada com sucesso!', 'sucesso')",
                true);
            CarregarCategoria();
        }

        public void btnSalvarAlteracao_Click(object sender, EventArgs e)
        {
            string categoriaAlteracao = txtNomeAlteracao.Text.Trim();
            if(string.IsNullOrEmpty(categoriaAlteracao))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "alertaNomeVazio",
                    "mostrarAlerta('O nome da categoria é obrigatória.', 'erro');",
                    true);
                return;
            }

            if(_categoriaDAL.CategoriaDuplicada(categoriaAlteracao))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "AlertaDuplicado",
                    "mostrarAlerta('Categoria duplicada', 'warning');",
                    true);
                return;
            }

            if (ViewState["CategoriaId"] == null)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "AlertaDuplicado",
                    "mostrarAlerta('Nenhuma Categoria Selecionada', 'warning');",
                    true);
                return;
            }

            int categoriaId = Convert.ToInt32(ViewState["CategoriaId"]);
            var categoriaAtual = _categoriaDAL.CarregarCategoria(categoriaId);

            var novaCategoria = new Models.Categoria
            {
                Id = categoriaId,
                Nome = categoriaAlteracao,
                Ativo = categoriaAtual.Ativo,
                DataAlteracao = DateTime.Now,
                UsuarioAlteracao = 1
            };

            _categoriaDAL.SalvarCategoria(novaCategoria);
            ViewState["CategoriaId"] = null;
            txtNome.Text = string.Empty;

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "alertaSucesso",
                "mostrarAlerta('Cadastrado com Sucesso!', 'sucesso');",
                true);

            blocoAlteracao.Visible = false;
            CarregarCategoria();
        }

        public void btnEditar_Click(object sender, EventArgs e)
        {
            foreach(GridViewRow row in gridCategorias.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridCategorias.DataKeys[row.RowIndex].Value);
                    var categoria = _categoriaDAL.CarregarCategoria(id);

                    txtNome.Text = categoria.Nome;
                    ViewState["CategoriaId"] = categoria.Id;

                    txtNome.Text = string.Empty;
                    blocoAlteracao.Visible = true;
                    break;
                }
            }
        }

        public void btnInativar_Click(object sender, EventArgs e)
        {
            bool algumSelecionado = false;

            foreach (GridViewRow row in gridCategorias.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelecionado");
                if(chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gridCategorias.DataKeys[row.RowIndex].Value);
                    var categoria = _categoriaDAL.CarregarCategoria(id);

                    txtNome.Text = categoria.Nome;
                    ViewState["CategoriaId"] = categoria.Id;

                    categoria.Ativo = false;
                    categoria.DataAlteracao = DateTime.Now;
                    categoria.UsuarioAlteracao = 1;

                    _categoriaDAL.SalvarCategoria(categoria);
                    algumSelecionado = true;
                }
            }

            if (algumSelecionado)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "categoriaInativada",
                    "mostrarAlerta('Categoria Inativada com sucesso', 'sucesso');",
                    true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "categoriaInativado",
                    "mostrarAlerta('Nenhum categoria selecionada', 'warning');",
                    true);
            }

            txtNome.Text = string.Empty;
            ViewState["CategoriaId"] = null;
            CarregarCategoria();
        }


        private void CarregarCategoria()
        {
            var listaCategoria = _categoriaDAL.ListarCategorias();
            var categoriaAtivo = listaCategoria.Where(p => p.Ativo).ToList();
            foreach(var p in listaCategoria)
            {
                System.Diagnostics.Debug.WriteLine($"Perfil => Id:{p.Id}, Nome:{p.Nome}");
            }
            gridCategorias.DataSource = categoriaAtivo;
            gridCategorias.DataBind();
        }
    }
}