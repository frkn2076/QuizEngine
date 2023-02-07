namespace Quiz.Engine.ViewModels.Requests;

public class QuizDetailRequestModel
{
    public string Title { get; set; }

    public IEnumerable<QuestionDetailRequestModel> Questions { get; set; }

    public bool IsPublished { get; set; }
}
