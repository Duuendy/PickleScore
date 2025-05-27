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
    public class CampeonatoDAL
    {
        private readonly string _connectionString;
        public CampeonatoDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public void CadastrarCampeonato(Campeonato campeonato)
        {
            using(IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                if (campeonato.Id == 0)
                {
                    string query = @"INSERT INTO campeonato (Nome, CategoriaId, Local, DataInicio, DataFim, DataInsercao, DataAlteracao) 
                                    VALUES (@Nome, @CategoriaId, @Local, @DataInicio, @DataFim, @DataInsercao, @DataAlteracao)";
                    campeonato.DataInsercao = DateTime.Now;
                    campeonato.DataAlteracao = DateTime.Now;
                    connection.Execute(query, campeonato);
                }
                else
                {
                    AtualizarCampeonato(campeonato);
                }
            }
        }

        public Campeonato CarregarCampeonato(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM campeonato WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Campeonato>(query, new { Id = id });
            }
        }

        public void AtualizarCampeonato(Campeonato campeonato)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                campeonato.DataAlteracao = DateTime.Now;
                string query = @"UPDATE campeonato 
                                SET Nome = @Nome, 
                                Categoria = @Categoria, 
                                Local = @Local, 
                                DataInicio = @DataInicio, 
                                DataFim = @DataFim, 
                                DataAlteracao = @DataAlteracao 
                                WHERE Id = @Id";
                campeonato.DataAlteracao = DateTime.Now;
                connection.Execute(query, campeonato);
            }
        }

        public void DeletarCampeonato(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"DELETE FROM campeonato WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }

        public List<Campeonato> ListarCampeonatos()
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM campeonato";
                return connection.Query<Campeonato>(query).ToList();
            }
        }        
    }
}