using Quiz.Engine.Service.DTOs;
using Quiz.Engine.ViewModels.Responses;

namespace Quiz.Engine.Service.Contracts;

public interface IQuizManagerService
{
    public Task<Guid> CreateQuizAsync(QuizDTO quizDTO, Guid userId);

    public Task UpdateQuizAsync(QuizDTO quizDTO, Guid quizId, Guid userId);

    public Task DeleteQuizAsync(Guid quizId, Guid userId);

    public Task<IEnumerable<QuizSummaryDTO>> GetOtherUserQuizzesAsync(Guid userId);

    public Task<QuizDTO> GetQuizAsync(Guid quizId, Guid userId);

    public Task<QuizExamResponseModel> SolveQuizAsync(QuizExamDTO quizExamDTO, Guid userId);

    public Task<IEnumerable<SolutionDetailDTO>> GetSolutionsOfUserAsync(Guid userId);

    public Task<QuizSolutionDTO> GetSolutionsOfQuizAsync(Guid quizId, Guid userId);

    public Task<IEnumerable<QuizSummaryDTO>> GetQuizzesOfUserAsync(Guid userId);
}
