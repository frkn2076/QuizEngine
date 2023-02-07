namespace Quiz.Engine.Service.DTOs;

public class SolutionDetailDTO
{
    public string QuizTitle { get; set; }

    public IEnumerable<SolutionQuestionDetailDTO> Questions { get; set; }
}