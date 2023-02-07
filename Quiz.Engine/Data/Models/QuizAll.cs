namespace Quiz.Engine.Data.Models;

public class QuizAll
{
    public Guid QuizId { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; }

    public bool IsPublished { get; set; }

    public Guid QuestionId { get; set; }

    public string Question { get; set; }

    public Guid AnswerId { get; set; }

    public string Answer { get; set; }

    public bool IsCorrectAnswer { get; set; }
}
