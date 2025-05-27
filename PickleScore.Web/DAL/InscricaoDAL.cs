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
    public class InscricaoDAL
    {
        private readonly string _connectionString;

        public InscricaoDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public void CadastrarInscricao(Inscricao inscricao)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                if (inscricao.Id == 0)
                {
                    string query = @"INSERT INTO inscricao (
                                        UsuarioId, UsuarioParceiroId,
                                        CampeonatoId, CategoriaId, NivelId, FaixaEtariaId,
                                        Valor, FormaPagamento,
                                        DataInicio, DataFim,
                                        DataInsercao, DataAlteracao) 
                                    VALUES (
                                        @UsuarioId, @UsuarioParceiroId,
                                        @CampeonatoId, @CategoriaId, @NivelId, @FaixaEtariaId,
                                        @Valor, @FormaPagamento,
                                        @DataInicio, @DataFim,
                                        @DataInsercao, @DataAlteracao)";
                    inscricao.DataInsercao = DateTime.Now;
                    inscricao.DataAlteracao = DateTime.Now;
                    connection.Execute(query, inscricao);
                }
                else
                {
                    AtualizarInscricao(inscricao);
                }
            }
        }

        public Inscricao CarregarInscricao(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM inscricao WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Inscricao>(query, new { Id = id });
            }
        }

        public void AtualizarInscricao(Inscricao inscricao)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                inscricao.DataAlteracao = DateTime.Now;
                string query = @"UPDATE inscricao 
                                SET UsuarioId = @UsuarioId, 
                                    UsuarioParceiroId = @UsuarioParceiroId,
                                    CampeonatoId = @CampeonatoId, 
                                    CategoriaId = @CategoriaId,
                                    NivelId = @NivelId,
                                    FaixaEtariaId = @FaixaEtariaId,
                                    Valor = @Valor,
                                    FormaPagamento = @FormaPagamento,
                                    DataInicio = @DataInicio,
                                    DataFim = @DataFim,
                                    DataAlteracao = @DataAlteracao 
                                WHERE Id = @Id";
                inscricao.DataAlteracao = DateTime.Now;
                connection.Execute(query, inscricao);
            }
        }

        public void DeletarInscricao(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"DELETE FROM inscricao WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }

        public List<Inscricao> ListarInscricoes()
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM inscricao ORDER BY DataInicio DESC";
                return connection.Query<Inscricao>(query).ToList();
            }
        }
    }
}