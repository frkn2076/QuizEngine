namespace Quiz.Engine.Service.DTOs;

public class SolutionQuestionDetailDTO
{
    public string Question { get; set; }

    public IEnumerable<string> Answers { get; set; }

    public double Point { get; set; }
}
