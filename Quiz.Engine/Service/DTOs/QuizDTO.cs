namespace Quiz.Engine.Service.DTOs;

public class QuizDTO
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public IEnumerable<QuestionDTO> Questions { get; set; }

    public bool IsPublished { get; set; }
}
