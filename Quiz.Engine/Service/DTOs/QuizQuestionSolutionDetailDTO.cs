namespace Quiz.Engine.Service.DTOs;

public class QuizQuestionSolutionDetailDTO
{
    public string Question { get; set; }

    public IEnumerable<string> Answers { get; set; }

    public double Point { get; set; }
}
