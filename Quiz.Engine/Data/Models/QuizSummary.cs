namespace Quiz.Engine.Data.Models;

public class QuizSummary
{
    public Guid QuizId { get; set; }

    public string Title { get; set; }

    public int QuestionCount { get; set; }
}