namespace Quiz.Engine.Data.Entities;

public class Solution
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid QuizId { get; set; }

    public Guid QuestionId { get; set; }

    public Guid AnswerId { get; set; }

    public double Point { get; set; }
}
