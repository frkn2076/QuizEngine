namespace Quiz.Engine.ViewModels.Responses;

public class QuizSolutionDetailResponseModel
{
    public Guid UserId { get; set; }

    public IEnumerable<QuizQuestionSolutionDetailResponseModel> Questions { get; set; }
}
