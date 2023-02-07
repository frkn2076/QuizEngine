namespace Quiz.Engine.Service.DTOs;

public class QuizSolutionDTO
{
    public string QuizTitle { get; set; }

    public IEnumerable<QuizSolutionDetailDTO> UserSolutions { get; set; }
}