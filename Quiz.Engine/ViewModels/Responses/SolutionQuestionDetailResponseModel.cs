namespace Quiz.Engine.ViewModels.Responses;

public class SolutionQuestionDetailResponseModel
{
    public string Question { get; set; }

    public IEnumerable<string> Answers { get; set; }

    public double Point { get; set; }
}
