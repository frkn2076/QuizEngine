namespace Quiz.Engine.Service.DTOs;

public class QuizExamDTO
{
    public Guid Id { get; set; }

    public IEnumerable<QuestionExamDTO> Questions { get; set; }
}
