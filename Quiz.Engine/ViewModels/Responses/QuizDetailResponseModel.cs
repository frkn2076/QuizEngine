namespace Quiz.Engine.ViewModels.Responses;

public class QuizDetailResponseModel
{
    public string Title { get; set; }

    public IEnumerable<QuestionDetailResponseModel> Questions { get; set; }
}
