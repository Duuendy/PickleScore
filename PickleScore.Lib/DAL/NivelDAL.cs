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
                    string query = @"INSERT INTO nivel (Nome, Ativo, DataInsercao, UsuarioInsercao, DataAlteracao, UsuarioAlteracao) 
                                 VALUES (@Nome, @Ativo, @DataInsercao, @UsuarioInsercao, @DataAlteracao, @UsuarioAlteracao)";
                    nivel.UsuarioInsercao = 1;
                    nivel.DataInsercao = DateTime.Now;
                    nivel.DataAlteracao = DateTime.Now;
                    connection.Execute(query, nivel);
                }
                else
                {
                    atualizarNivel(nivel);
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
        private void atualizarNivel(Nivel nivel)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"UPDATE nivel 
                                 SET Nome = @Nome, 
                                 Ativo = @Ativo,
                                 DataAlteracao = @DataAlteracao,
                                 UsuarioAlteracao = @UsuarioAlteracao
                                 WHERE Id = @Id";
                nivel.DataAlteracao = DateTime.Now;
                nivel.UsuarioAlteracao = 1;
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

        public bool NivelDuplicado(string nome, int? idAtual = null)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string nomeNormalizado = nome.ToLowerInvariant().Trim();

                string query = @"SELECT COUNT(*) FROM nivel WHERE LOWER(Nome) = @nome AND Ativo = 1";

                if (idAtual.HasValue)
                {
                    query += " AND Id <> @idAtual";
                }

                int count = connection.ExecuteScalar<int>(query, new { nome = nomeNormalizado, idAtual });
                return count > 0;
            }
        }

    }
}