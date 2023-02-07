namespace Quiz.Engine.ViewModels.Responses;

public class QuizDetailSummaryResponseModel
{
    public QuizDetailSummaryResponseModel(Guid quizId)
    {
        QuizId = quizId;
    }

    public Guid QuizId { get; set; }
}
