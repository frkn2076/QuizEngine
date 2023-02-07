namespace Quiz.Engine.ViewModels.Requests;

public class QuestionExamRequestModel
{
    public string Id { get; set; }

    public IEnumerable<string> Answers { get; set; }
}
