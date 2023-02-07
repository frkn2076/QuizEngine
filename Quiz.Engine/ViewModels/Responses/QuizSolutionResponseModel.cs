namespace Quiz.Engine.ViewModels.Responses;

public class QuizSolutionResponseModel
{
    public string QuizTitle { get; set; }

    public IEnumerable<QuizSolutionDetailResponseModel> UserSolutions { get; set; }
}
