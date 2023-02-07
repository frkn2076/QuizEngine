namespace Quiz.Engine.ViewModels.Requests;

public class QuizExamRequestModel
{
    public string Id { get; set; }

    public IEnumerable<QuestionExamRequestModel> Questions { get; set; }
}
