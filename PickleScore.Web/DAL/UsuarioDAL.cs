using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using PickleScore.Web.Models;
using System.Threading;


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
                    string query = @"INSERT INTO usuario (Nome, Sobrenome, Senha, Cpf, Email, Nascimento, Ativo, PerfilId , DataInsercao, UsuarioInsercao, DataAlteracao, UsuarioAlteracao) 
                                VALUES (@Nome, @Sobrenome, @Senha, @Cpf, @Email, @Nascimento, @Ativo, @PerfilId, @DataInsercao, @UsuarioInsercao, @DataAlteracao, @UsuarioAlteracao)";
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
                                    Sobrenome = @Sobrenome,
                                    Senha = @Senha, 
                                    Cpf = @Cpf, 
                                    Email = @Email, 
                                    Nascimento = @Nascimento, 
                                    PerfilId = @PerfilId,
                                    Ativo = @Ativo,
                                    DataAlteracao = @DataAlteracao,
                                    UsuarioAlteracao = @UsuarioAlteracao
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

        public bool UsuarioDuplicado(string nome)
        {
            using(IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string nomeNormalizado = nome.ToLowerInvariant().Normalize();

                string query = @"SELECT COUNT(*) FROM usuario WHERE LOWER(Nome) = @NomeNormalizado";

                int count = connection.ExecuteScalar<int>(query, new { nome = nomeNormalizado });
                return count > 0;
            }
        }

        public bool UsuarioCpfDuplicado(string cpf, int? idUsuario)
        {
            using(IDbConnection connection = new MySqlConnection(_connectionString))
            {
                string query = idUsuario.HasValue
                    ? "SELECT COUNT(*) FROM usuario WHERE Cpf = @Cpf AND Id <> @IdAtual"
                    : "SELECT COUNT(*) FROM usuario WHERE Cpf = @Cpf";

                Console.WriteLine($"Verificando CPF duplicado: CPF={cpf}, ID={idUsuario}");
                int count = connection.ExecuteScalar<int>(query, new { Cpf = cpf, IdAtual = idUsuario });
                return count > 0;
            }
        }

        public bool UsuarioEmailDuplicado(string email, int? idUsuario)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                string query = idUsuario.HasValue
                    ? "SELECT COUNT(*) FROM Usuario WHERE Email = @Email AND Id <> @IdUsuario)"
                    : "SELECT COUNT(*) FROM Usuario WHERE Email = @Email";

                int count = conexao.ExecuteScalar<int>(query, new { Email = email, IdUsuario = idUsuario });
                return count > 0;
            }
        }
    }
}