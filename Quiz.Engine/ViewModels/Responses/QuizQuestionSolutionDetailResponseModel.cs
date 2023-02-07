namespace Quiz.Engine.ViewModels.Responses;

public class QuizQuestionSolutionDetailResponseModel
{
    public string Question { get; set; }

    public IEnumerable<string> Answers { get; set; }

    public double Point { get; set; }
}
