namespace Quiz.Engine.ViewModels.Responses;

public class QuestionDetailResponseModel
{
    public Guid Id { get; set; }

    public string Question { get; set; }

    public bool IsSingleChoice { get; set; }

    public IEnumerable<AnswerDetailResponseModel> Answers { get; set; }
}
