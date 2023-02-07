namespace Quiz.Engine.Service.DTOs;

public class QuizSummaryDTO
{
    public Guid QuizId { get; set; }

    public string Title { get; set; }

    public int QuestionCount { get; set; }

    public bool IsPublished { get; set; }
}
