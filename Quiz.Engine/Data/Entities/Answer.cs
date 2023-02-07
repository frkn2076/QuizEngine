namespace Quiz.Engine.Data.Entities;

public class Answer
{
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }

    public string Text { get; set; }

    public bool IsCorrectAnswer { get; set; }
}
