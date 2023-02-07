using Dapper;
using Npgsql;
using Quiz.Engine.Data.Entities;
using Quiz.Engine.Data.Enums;
using Quiz.Engine.Data.Models;
using Quiz.Engine.Data.Repositories.Contracts;
using System.Data;

namespace Quiz.Engine.Data.Repositories.Implementations;

public class QuizEngineRepository : IQuizEngineRepository
{
    private readonly string _connectionString;

    public QuizEngineRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("QuizEngineContext");
    }

    public IDbConnection CreateDbConnection() => new NpgsqlConnection(_connectionString);

    public async Task<Guid> CreateUserAsync(User user, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.CreateUserQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryFirstOrDefaultAsync<Guid>(query, user, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Guid>(query, user, transaction: transaction);
        }
    }

    public async Task<Guid> GetUserAsync(User user, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.GetUserQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryFirstOrDefaultAsync<Guid>(query, user, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Guid>(query, user, transaction: transaction);
        }
    }

    public async Task<Guid> GetUserAsync(string email, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.GetUserByEmailQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryFirstOrDefaultAsync<Guid>(query, new { email }, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Guid>(query, new { email }, transaction: transaction);
        }
    }

    public async Task<Guid> CreateQuizAsync(Entities.Quiz quiz, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.CreateQuizQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryFirstOrDefaultAsync<Guid>(query, quiz, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Guid>(query, quiz, transaction: transaction);
        }
    }

    public async Task<Guid> CreateQuestionAsync(Question question, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.CreateQuestionQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryFirstOrDefaultAsync<Guid>(query, question, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Guid>(query, question, transaction: transaction);
        }
    }

    public async Task<Guid> CreateAsnwerAsync(Answer answer, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.CreateAnswerQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryFirstOrDefaultAsync<Guid>(query, answer, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Guid>(query, answer, transaction: transaction);
        }
    }

    public async Task<Entities.Quiz> GetQuizAsync(Guid quizId, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.GetQuizQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryFirstOrDefaultAsync<Entities.Quiz>(query, new { quizId }, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Entities.Quiz>(query, new { quizId }, transaction: transaction);
        }
    }

    public async Task<bool> UpdateQuizAsync(Entities.Quiz quiz, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.UpdateQuizQuery);

        if (IsConnectionActive(dbConnection))
        {
            var affectedRows = await dbConnection.ExecuteAsync(query, quiz, transaction: transaction);
            return affectedRows > 0;
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            var affectedRows = await connection.ExecuteAsync(query, quiz, transaction: transaction);
            return affectedRows > 0;
        }
    }

    public async Task<IEnumerable<Guid>> DeleteQuestionAsync(Guid quizId, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.DeleteQuestionQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryAsync<Guid>(query, new { quizId }, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<Guid>(query, new { quizId }, transaction: transaction);
        }
    }

    public async Task<bool> DeleteAnswerAsync(IList<Guid> questionIds, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.DeleteAnswerQuery);

        if (IsConnectionActive(dbConnection))
        {
            var affectedRows = await dbConnection.ExecuteAsync(query, new { questionIds }, transaction: transaction);
            return affectedRows > 0;
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            var affectedRows = await connection.ExecuteAsync(query, new { questionIds }, transaction: transaction);
            return affectedRows > 0;
        }
    }

    public async Task<bool> DeleteQuizAsync(Guid quizId, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.DeleteQuizQuery);

        if (IsConnectionActive(dbConnection))
        {
            var affectedRows = await dbConnection.ExecuteAsync(query, new { quizId }, transaction: transaction);
            return affectedRows > 0;
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            var affectedRows = await connection.ExecuteAsync(query, new { quizId }, transaction: transaction);
            return affectedRows > 0;
        }
    }

    public async Task<IEnumerable<QuizSummary>> GetOtherUsersQuizzesAsync(Guid userId, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.GetOtherUsersQuizzesQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryAsync<QuizSummary>(query, new { userId }, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<QuizSummary>(query, new { userId }, transaction: transaction);
        }
    }

    public async Task<QuestionType?> GetQuestionTypeAsync(Guid questionId, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.GetQuestionTypeQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryFirstOrDefaultAsync<QuestionType?>(query, new { questionId }, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<QuestionType?>(query, new { questionId }, transaction: transaction);
        }
    }

    public async Task<Guid> CreateSolutionAsync(Solution solution, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.CreateSolutionQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryFirstOrDefaultAsync<Guid>(query, solution, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Guid>(query, solution, transaction: transaction);
        }
    }

    public async Task<IEnumerable<Solution>> GetSolutionsAsync(Guid userId, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.GetSolutionsQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryAsync<Solution>(query, new { userId }, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<Solution>(query, new { userId }, transaction: transaction);
        }
    }

    public async Task<IEnumerable<SolutionDetail>> GetSolutionDetailsAsync(Guid userId, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.GetSolutionDetailsQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryAsync<SolutionDetail>(query, new { userId }, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<SolutionDetail>(query, new { userId }, transaction: transaction);
        }
    }

    public async Task<IEnumerable<SolutionDetail>> GetSolutionDetailsOfQuizAsync(Guid quizId, Guid userId, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.GetSolutionDetailsOfQuizQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryAsync<SolutionDetail>(query, new { quizId, userId }, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<SolutionDetail>(query, new { quizId, userId }, transaction: transaction);
        }
    }

    public async Task<IEnumerable<QuizAll>> GetQuizAllAsync(Guid quizId, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.GetQuizAllQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryAsync<QuizAll>(query, new { quizId }, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<QuizAll>(query, new { quizId }, transaction: transaction);
        }
    }

    public async Task<User> GetUserByIdAsync(Guid id, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.GetUserByIdQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryFirstOrDefaultAsync<User>(query, new { id }, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { id }, transaction: transaction);
        }
    }

    public async Task<IEnumerable<QuizDetail>> GetQuizzesOfUserAsync(Guid userId, IDbConnection dbConnection = null, IDbTransaction transaction = null)
    {
        var query = GetQuery(Constants.Queries.GetQuizzesOfUserQuery);

        if (IsConnectionActive(dbConnection))
        {
            return await dbConnection.QueryAsync<QuizDetail>(query, new { userId }, transaction: transaction);
        }

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<QuizDetail>(query, new { userId }, transaction: transaction);
        }
    }

    #region Helper

    private string GetQuery(string queryFileName)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var path = Path.Combine(currentDirectory, Constants.AdhocFolderPath);
        var file = string.Concat(queryFileName, Constants.SqlFileExtension);
        var filePath = Path.Combine(path, file);
        return File.ReadAllText(filePath);
    }

    private bool IsConnectionActive(IDbConnection dbConnection)
    {
        return dbConnection is not null && dbConnection.State == ConnectionState.Open;
    }

    #endregion Helper
}
