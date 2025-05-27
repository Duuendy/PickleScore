using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using PickleScore.Web.Models;

namespace PickleScore.Web.DAL
{
    public class CategoriaDAL
    {
        private readonly string _connectionString;

        public CategoriaDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public void SalvarCategoria(Categoria categoria)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                if (categoria.Id == 0)
                {
                    string query = @"INSERT INTO categoria (Nome, DataInsercao, DataAlteracao) 
                                  VALUES (@Nome, @DataInsercao, @DataAlteracao)";
                    categoria.DataInsercao = DateTime.Now;
                    categoria.DataAlteracao = DateTime.Now;
                    connection.Execute(query, categoria);
                }
                else
                {
                    AtualizarCategoria(categoria);
                }
            }
        }

        public Categoria CarregarCategoria(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM categoria WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Categoria>(query, new { Id = id });
            }
        }

        public void AtualizarCategoria(Categoria categoria)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                categoria.DataAlteracao = DateTime.Now;
                string query = @"UPDATE categoria 
                                  SET Nome = @Nome, 
                                  DataAlteracao = @DataAlteracao 
                                  WHERE Id = @Id";
                connection.Execute(query, categoria);
            }
        }

        public void DeletarCategoria(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"DELETE FROM categoria WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }
    
        public List<Categoria> ListarCategorias()
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM categoria";
                return connection.Query<Categoria>(query).ToList();
            }
        } 
    }
}