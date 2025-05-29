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
    public class FormaPagamentoDAL
    {
        private readonly string _connectionString;

        public FormaPagamentoDAL()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public void CadastrarFormaPagamento(FormaPagamento formaPagamento)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                connection.Open();
                if (formaPagamento.Id == 0)
                {
                    string query = @"INSERT INTO tblformapagamento (Nome, DataInsercao, DataAlteracao) 
                                 VALUES (@Nome, @DataInsercao, @DataAlteracao)";
                    formaPagamento.DataInsercao = DateTime.Now;
                    formaPagamento.DataAlteracao = DateTime.Now;
                    connection.Execute(query, formaPagamento);
                }
                else
                {
                    AtualizarFormaPagamento(formaPagamento);
                }
            }
        }
        public FormaPagamento CarregarFormaPagamento(int id)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM forma_pagamento WHERE Id = @Id";
                return connection.QueryFirstOrDefault<FormaPagamento>(query, new { Id = id });
            }
        }
        public void AtualizarFormaPagamento(FormaPagamento formaPagamento)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                string query = @"UPDATE forma_pagamento 
                                 SET Nome = @Nome, DataAlteracao = @DataAlteracao 
                                 WHERE Id = @Id";
                formaPagamento.DataAlteracao = DateTime.Now;
                connection.Execute(query, formaPagamento);
            }
        }

        public void ExcluirFormaPagamento(int id)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                string query = @"DELETE FROM forma_pagamento WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }
        public List<FormaPagamento> ListarFormasPagamento()
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM forma_pagamento";
                return connection.Query<FormaPagamento>(query).ToList();
            }
        }
    }
}