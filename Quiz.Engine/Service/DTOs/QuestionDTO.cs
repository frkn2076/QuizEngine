namespace Quiz.Engine.Service.DTOs;

public class QuestionDTO
{
    public Guid Id { get; set; }

    public string Question { get; set; }

    public IEnumerable<AnswerDTO> Answers { get; set; }
}
