namespace Quiz.Engine.Service.DTOs;

public class QuestionExamDTO
{
    public Guid Id { get; set; }

    public IEnumerable<Guid> Answers { get; set; }
}
