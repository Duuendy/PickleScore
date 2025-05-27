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
    public class NivelDAL
    {
        private readonly string _connectionString;

        public NivelDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public void SalvarNivel(Nivel nivel)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                if (nivel.Id == 0)
                {
                    string query = @"INSERT INTO nivel (Nome, DataInsercao, DataAlteracao) 
                                 VALUES (@Nome, @DataInsercao, @DataAlteracao)";
                    nivel.DataInsercao = DateTime.Now;
                    nivel.DataAlteracao = DateTime.Now;
                    connection.Execute(query, nivel);
                }
                else
                {
                    AtualizarNivel(nivel);
                }
            }
        }

        public Nivel CarregarNivel(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM nivel WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Nivel>(query, new { Id = id });
            }
        }
        public void AtualizarNivel(Nivel nivel)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                nivel.DataAlteracao = DateTime.Now;
                string query = @"UPDATE nivel 
                                 SET Nome = @Nome, 
                                 DataAlteracao = @DataAlteracao 
                                 WHERE Id = @Id";
                connection.Execute(query, nivel);
            }
        }
        public void DeletarNivel(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"DELETE FROM nivel WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }
        public List<Nivel> ListarNiveis()
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM nivel";
                return connection.Query<Nivel>(query).ToList();
            }
        } 
    }
}