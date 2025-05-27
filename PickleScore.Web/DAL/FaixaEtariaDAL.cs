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
    public class FaixaEtariaDAL
    {
        private readonly string _connectionString;

        public FaixaEtariaDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public void SalvarFaixaEtaria(FaixaEtaria faixaEtaria)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                if (faixaEtaria.Id == 0)
                {
                    string query = @"INSERT INTO faixaetaria (Nome, DataInsercao, DataAlteracao) 
                                  VALUES (@Nome, @DataInsercao, @DataAlteracao)";
                    faixaEtaria.DataInsercao = DateTime.Now;
                    faixaEtaria.DataAlteracao = DateTime.Now;
                    connection.Execute(query, faixaEtaria);
                }
                else
                {
                    AtualizarFaixaEtaria(faixaEtaria);
                }
            }
        }

        public FaixaEtaria CarregarFaixaEtaria(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM faixaetaria WHERE Id = @Id";
                return connection.QueryFirstOrDefault<FaixaEtaria>(query, new { Id = id });
            }
        }

        public void AtualizarFaixaEtaria(FaixaEtaria faixaEtaria)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                faixaEtaria.DataAlteracao = DateTime.Now;
                string query = @"UPDATE faixaetaria 
                                  SET Nome = @Nome, 
                                  DataAlteracao = @DataAlteracao 
                                  WHERE Id = @Id";
                connection.Execute(query, faixaEtaria);
            }
        }

        public void DeletarFaixaEtaria(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"DELETE FROM faixaetaria WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }

        public List<FaixaEtaria> ListarFaixasEtarias()
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM faixaetaria";
                return connection.Query<FaixaEtaria>(query).ToList();
            }
        }
    }
}