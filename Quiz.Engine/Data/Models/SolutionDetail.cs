namespace Quiz.Engine.Data.Models;

public class SolutionDetail
{
    public Guid QuizId { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; }

    public Guid QuestionId { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }

    public double Point { get; set; }
}
