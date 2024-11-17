using System;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace GlobalSolution.Services
{
    public class UsuarioEnergiaService
    {
        private readonly string _connectionString;

        public UsuarioEnergiaService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> InserirUsuarioAsync(string nome, string email, string senha)
        {
            using (var connection = new OracleConnection(_connectionString))
            using (var command = new OracleCommand("inserir_usuario_energia", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("p_nome", OracleDbType.Varchar2).Value = nome;
                command.Parameters.Add("p_email", OracleDbType.Varchar2).Value = email;
                command.Parameters.Add("p_senha", OracleDbType.Varchar2).Value = senha;

                var mensagemParam = new OracleParameter("p_mensagem", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(mensagemParam);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return mensagemParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    return $"Erro ao inserir usuário: {ex.Message}";
                }
            }
        }
    }
}
