namespace Quiz.Engine.Service.DTOs;

public class QuizSolutionDetailDTO
{
    public Guid UserId { get; set; }

    public IEnumerable<QuizQuestionSolutionDetailDTO> Questions { get; set; }
}
