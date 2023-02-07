using Quiz.Engine.Data.Entities;
using Quiz.Engine.Data.Enums;
using Quiz.Engine.Data.Models;
using System.Data;

namespace Quiz.Engine.Data.Repositories.Contracts;

public interface IQuizEngineRepository
{
    IDbConnection CreateDbConnection();

    public Task<Guid> CreateUserAsync(User user, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<Guid> GetUserAsync(string email, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<Guid> GetUserAsync(User user, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<Guid> CreateQuizAsync(Entities.Quiz quiz, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<Guid> CreateQuestionAsync(Question question, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<Guid> CreateAsnwerAsync(Answer answer, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<Entities.Quiz> GetQuizAsync(Guid quizId, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<bool> UpdateQuizAsync(Entities.Quiz quiz, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<IEnumerable<Guid>> DeleteQuestionAsync(Guid quizId, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<bool> DeleteAnswerAsync(IList<Guid> questionIds, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<bool> DeleteQuizAsync(Guid quizId, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<IEnumerable<QuizSummary>> GetOtherUsersQuizzesAsync(Guid userId, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<Guid> CreateSolutionAsync(Solution solution, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<IEnumerable<Solution>> GetSolutionsAsync(Guid userId, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<IEnumerable<SolutionDetail>> GetSolutionDetailsAsync(Guid userId, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<IEnumerable<SolutionDetail>> GetSolutionDetailsOfQuizAsync(Guid quizId, Guid userId, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<QuestionType?> GetQuestionTypeAsync(Guid questionId, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<IEnumerable<QuizAll>> GetQuizAllAsync(Guid quizId, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<User> GetUserByIdAsync(Guid id, IDbConnection dbConnection = null, IDbTransaction transaction = null);

    public Task<IEnumerable<QuizDetail>> GetQuizzesOfUserAsync(Guid userId, IDbConnection dbConnection = null, IDbTransaction transaction = null);
}
