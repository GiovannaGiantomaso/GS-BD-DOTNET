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

        // Método para chamar a procedure de inserir um novo usuário
        public async Task<string> InserirUsuarioAsync(string nome, string email, string senha)
        {
            using (var connection = new OracleConnection(_connectionString))
            using (var command = new OracleCommand("inserir_usuario_energia", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Define os parâmetros da procedure
                command.Parameters.Add("p_nome", OracleDbType.Varchar2).Value = nome;
                command.Parameters.Add("p_email", OracleDbType.Varchar2).Value = email;
                command.Parameters.Add("p_senha", OracleDbType.Varchar2).Value = senha;

                // Parâmetro de saída para a mensagem de retorno
                var mensagemParam = new OracleParameter("p_mensagem", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(mensagemParam);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    // Retorna a mensagem recebida da procedure
                    return mensagemParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    return $"Erro ao inserir usuário: {ex.Message}";
                }
            }
        }


        // Método para chamar a procedure de inserir um novo consumo
        public async Task<string> InserirConsumoAsync(int idUsuario, decimal consumoKwh)
        {
            using (var connection = new OracleConnection(_connectionString))
            using (var command = new OracleCommand("inserir_consumo_energia", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("p_id_usuario", OracleDbType.Int32).Value = idUsuario;
                command.Parameters.Add("p_consumo_kwh", OracleDbType.Decimal).Value = consumoKwh;

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return "Consumo inserido com sucesso.";
                }
                catch (OracleException ex) when (ex.Number == 1403) // 1403: Código para NO_DATA_FOUND
                {
                    return $"Erro: ID de usuário '{idUsuario}' não encontrado.";
                }
                catch (Exception ex)
                {
                    return $"Erro ao inserir consumo de energia: {ex.Message}";
                }
            }
        }
    }
}
