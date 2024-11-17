using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using GlobalSolution.Repositories.Interfaces;
using GlobalSolution.Models;

namespace GlobalSolution.Services
{
    public class HistoricoConsumoService
    {
        private readonly string _connectionString;
        private readonly IHistoricoConsumoRepository _repository;

        public HistoricoConsumoService(string connectionString, IHistoricoConsumoRepository repository)
        {
            _connectionString = connectionString;
            _repository = repository;
        }

        public async Task<string> InserirHistoricoAsync(int idUsuario)
        {
            using (var connection = new OracleConnection(_connectionString))
            using (var command = new OracleCommand("inserir_historico_consumo", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("p_id_usuario", OracleDbType.Int32).Value = idUsuario;

                var mensagemParam = new OracleParameter("p_mensagem", OracleDbType.Varchar2, 255)
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

                    return $"Erro ao inserir histórico: {ex.Message}";
                }
            }
        }

        public async Task<IEnumerable<HistoricoConsumo>> BuscarTodosHistoricosAsync()
        {
            return await _repository.GetAllHistoricosAsync();
        }

        public async Task<HistoricoConsumo> BuscarHistoricoPorIdAsync(int id)
        {
            return await _repository.GetHistoricoByIdAsync(id);
        }

        public async Task<string> DeletarHistoricoAsync(int id)
        {
            var historico = await _repository.GetHistoricoByIdAsync(id);
            if (historico == null)
            {
                return "Erro: Histórico não encontrado.";
            }

            await _repository.DeleteHistoricoAsync(id);
            return "Histórico deletado com sucesso.";
        }
    }
}
