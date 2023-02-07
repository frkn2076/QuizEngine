namespace Quiz.Engine.Service.DTOs;

public class AnswerDTO
{
    public Guid Id { get; set; }

    public string Answer { get; set; }

    public bool IsCorrectAnswer { get; set; }
}
