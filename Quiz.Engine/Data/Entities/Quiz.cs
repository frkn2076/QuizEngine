namespace Quiz.Engine.Data.Entities;

public class Quiz
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; }

    public bool IsPublished { get; set; }
}
