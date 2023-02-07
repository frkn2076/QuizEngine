using Quiz.Engine.Data.Enums;

namespace Quiz.Engine.Data.Entities;

public class Question
{
    public Guid Id { get; set; }

    public Guid QuizId { get; set; }

    public string Text { get; set; }
    
    public QuestionType Type { get; set; }
}
