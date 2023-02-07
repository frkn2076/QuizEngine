namespace Quiz.Engine.ViewModels.Responses;

public class SolutionDetailResponseModel
{
    public string QuizTitle { get; set; }

    public IEnumerable<SolutionQuestionDetailResponseModel> Questions { get; set; }
}
