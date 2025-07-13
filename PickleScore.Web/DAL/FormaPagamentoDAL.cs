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

        public void SalvarFormaPagamento(FormaPagamento formaPagamento)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                connection.Open();
                if (formaPagamento.Id == 0)
                {
                    string query = @"INSERT INTO tblformapagamento (Nome, Ativo, DataInsercao, UsuarioInsercao, DataAlteracao, UsuarioAlteracao) 
                                 VALUES (@Nome, @Ativo, @DataInsercao, @UsuarioInsercao, @DataAlteracao, @UsuarioAlteracao)";
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
                string query = @"SELECT * FROM tblformapagamento WHERE Id = @Id";
                return connection.QueryFirstOrDefault<FormaPagamento>(query, new { Id = id });
            }
        }
        public void AtualizarFormaPagamento(FormaPagamento formaPagamento)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                string query = @"UPDATE tblformapagamento 
                                 SET Nome = @Nome, 
                                    Ativo = @Ativo,
                                    DataInsercao = @DataInsercao,
                                    UsuarioInsercao = @UsuarioInsercao,
                                    DataAlteracao = @DataAlteracao, 
                                    UsuarioAlteracao = @UsuarioAlteracao                   
                                 WHERE Id = @Id";
                formaPagamento.DataAlteracao = DateTime.Now;
                connection.Execute(query, formaPagamento);
            }
        }

        public void ExcluirFormaPagamento(int id)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                string query = @"DELETE FROM tblformapagamento WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }
        public List<FormaPagamento> ListarFormasPagamento()
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM tblformapagamento";
                return connection.Query<FormaPagamento>(query).ToList();
            }
        }

        public bool FormaPagamentoDuplicado(string nome, int? idAtual = null)
        {
            using(IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string nomeNormalizado = nome.ToLowerInvariant().Trim();

                string query = @"SELECT COUNT(*) FROM tblformapagamento WHERE LOWER(Nome) = @nome AND Ativo = 1";

                if (idAtual.HasValue)
                {
                    query += " AND Id <> @idAtual";
                }

                int count = connection.ExecuteScalar<int>(query, new { nome = nomeNormalizado, idAtual});
                return count > 0;
            }
        }
    }
}