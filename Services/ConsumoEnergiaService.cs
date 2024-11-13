using System;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace GlobalSolution.Services
{
    public class ConsumoEnergiaService
    {
        private readonly string _connectionString;

        public ConsumoEnergiaService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Método para chamar a procedure de inserir um novo consumo de energia
        public async Task<string> InserirConsumoAsync(int idUsuario, decimal consumoKwh)
        {
            using (var connection = new OracleConnection(_connectionString))
            using (var command = new OracleCommand("inserir_consumo_energia", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Define os parâmetros da procedure
                command.Parameters.Add("p_id_usuario", OracleDbType.Int32).Value = idUsuario;
                command.Parameters.Add("p_consumo_kwh", OracleDbType.Decimal).Value = consumoKwh;

                // Parâmetro OUT para a mensagem de retorno
                var mensagemParam = new OracleParameter("p_mensagem", OracleDbType.Varchar2, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(mensagemParam);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    // Retorna a mensagem recebida pelo parâmetro OUT
                    return mensagemParam.Value.ToString();
                }
                catch (Exception ex)
                {
                    return $"Erro ao inserir consumo de energia: {ex.Message}";
                }
            }
        }
    }
}
