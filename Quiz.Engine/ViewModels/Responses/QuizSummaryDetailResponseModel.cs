namespace Quiz.Engine.ViewModels.Responses;

public class QuizSummaryDetailResponseModel
{
    public Guid QuizId { get; set; }

    public string Title { get; set; }

    public int QuestionCount { get; set; }

    public bool IsPublished { get; set; }
}
