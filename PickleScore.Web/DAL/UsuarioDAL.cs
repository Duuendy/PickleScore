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
    public class UsuarioDAL
    {
        private readonly string _connectionString;

        public UsuarioDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }
        public void CadastrarUsuario(Usuario usuario)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                if (usuario.Id == 0)
                {
                    string query = @"INSERT INTO usuario (Nome, Senha, Cpf, Email, Nascimento, PerfilId , DataInsercao, DataAlteracao) 
                                VALUES (@Nome, @Senha, @Cpf, @Email, @Nascimento, @PerfilId, @DataInsercao, @DataAlteracao)";
                    usuario.DataInsercao = DateTime.Now;
                    usuario.DataAlteracao = DateTime.Now;
                    connection.Execute(query, usuario);
                }
                else
                {
                    AtualizarUsuario(usuario);
                }
            }
        }

        public Usuario CarregarUsuario(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM usuario WHERE id = @id";
                return connection.QueryFirstOrDefault<Usuario>(query, new {Id = id});
                
            }
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                usuario.DataAlteracao = DateTime.Now;
                string query = @"UPDATE usuario 
                                SET Nome = @Nome, 
                                    Senha = @Senha, 
                                    Cpf = @Cpf, 
                                    Email = @Email, 
                                    Nascimento = @Nascimento, 
                                    PerfilId = @PerfilId,
                                    DataAlteracao = @DataAlteracao
                                WHERE id = @id";
                connection.Execute(query, usuario);
            }
        }

        public void DeletarUsuario(int id)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"DELETE FROM usuario WHERE id = @id";
                connection.Execute(query, new { id });
            }
        }

        public List<Usuario> ListarUsuarios()
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM usuario";
                return connection.Query<Usuario>(query).ToList();
            }
        }
    }
}