using Dapper;
using MySql.Data.MySqlClient;
using PickleScore.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace PickleScore.Web.DAL
{
    public class PerfilDAL
    {
        private readonly string _connectionString;
        public PerfilDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public void SalvarPerfil(Perfil perfil)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                if (perfil.Id == 0)
                {
                    string query = @"INSERT INTO perfil (Nome, Ativo, DataInsercao, UsuarioInsercao, DataAlteracao, UsuarioAlteracao) 
                                VALUES (@Nome, @Ativo, @DataInsercao, @UsuarioInsercao, @DataAlteracao, UsuarioAlteracao)";

                    perfil.UsuarioInsercao = 1; // Substitua pelo usuário atual
                    perfil.DataInsercao = DateTime.Now;
                    perfil.DataAlteracao = DateTime.Now;
                    connection.Execute(query, perfil);

                }
                else
                {
                    atualizarPerfil(perfil);
                }

            }
        }

        public Perfil CarregarPerfil(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM perfil WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Perfil>(query, new { Id = id });
            }
        }

        private void atualizarPerfil(Perfil perfil)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"UPDATE perfil 
                                SET Nome = @Nome, 
                                Ativo = @Ativo,
                                DataAlteracao = @DataAlteracao,
                                UsuarioAlteracao = @UsuarioAlteracao        
                                WHERE Id = @Id";
                perfil.DataAlteracao = DateTime.Now;
                perfil.UsuarioAlteracao = 1;
                connection.Execute(query, perfil);
            }
        }

        public void DeletarPerfil(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"DELETE FROM perfil WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }

        public List<Perfil> ListarPerfis()
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM perfil";
                return connection.Query<Perfil>(query).ToList();
            }
        }

        public bool PerfilDuplicado(string nome)
        {
            using(IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string nomeNormalizado = nome.ToLowerInvariant().Normalize();

                string query = @"SELECT COUNT(*) FROM perfil WHERE LOWER(Nome) = @NomeNormalizado";

                int count = connection.ExecuteScalar<int>(query, new { NomeNormalizado = nomeNormalizado });
                return count > 0;
            }
        }
    }
}