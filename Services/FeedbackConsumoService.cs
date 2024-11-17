using GlobalSolution.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using GlobalSolution.Models;

namespace GlobalSolution.Services
{
    public class FeedbackConsumoService
    {
        private readonly string _connectionString;
        private readonly IFeedbackConsumoRepository _repository;

        public FeedbackConsumoService(string connectionString, IFeedbackConsumoRepository repository)
        {
            _connectionString = connectionString;
            _repository = repository;
        }

        public async Task<string> InserirFeedbackAsync(int idUsuario, string mensagemFeedback)
        {
            using (var connection = new OracleConnection(_connectionString))
            using (var command = new OracleCommand("inserir_feedback_consumo", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("p_id_usuario", OracleDbType.Int32).Value = idUsuario;
                command.Parameters.Add("p_mensagem_feedback", OracleDbType.Varchar2).Value = mensagemFeedback;

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
                    return $"Erro ao inserir feedback: {ex.Message}";
                }
            }
        }

        public async Task<FeedbackConsumo> GetFeedbackByIdAsync(int id)
        {
            return await _repository.GetFeedbackByIdAsync(id);
        }

        public async Task<string> DeleteFeedbackAsync(int id)
        {
            var feedback = await _repository.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return "Erro: Feedback não encontrado.";
            }

            await _repository.DeleteFeedbackAsync(id);
            return "Feedback deletado com sucesso.";
        }

        public async Task<IEnumerable<FeedbackConsumo>> GetAllFeedbacksAsync()
        {
            return await _repository.GetAllFeedbacksAsync();
        }
    }
}
