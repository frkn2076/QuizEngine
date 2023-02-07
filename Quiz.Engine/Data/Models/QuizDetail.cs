namespace Quiz.Engine.Data.Models;

public class QuizDetail
{
    public Guid QuizId { get; set; }

    public string Title { get; set; }

    public int QuestionCount { get; set; }

    public bool IsPublished { get; set; }
}
