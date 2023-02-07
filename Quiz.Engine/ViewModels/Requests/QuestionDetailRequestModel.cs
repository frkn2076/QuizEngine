namespace Quiz.Engine.ViewModels.Requests;

public class QuestionDetailRequestModel
{
    public string Question { get; set; }

    public IEnumerable<AnswerDetailRequestModel> Answers { get; set; }
}
