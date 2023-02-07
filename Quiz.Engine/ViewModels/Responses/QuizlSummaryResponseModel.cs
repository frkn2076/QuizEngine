namespace Quiz.Engine.ViewModels.Responses;

public class QuizSummaryResponseModel
{
    public Guid QuizId { get; set; }

    public string Title { get; set; }

    public int QuestionCount { get; set; }
}
