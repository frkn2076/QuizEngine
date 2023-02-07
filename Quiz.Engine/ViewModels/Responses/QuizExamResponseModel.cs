namespace Quiz.Engine.ViewModels.Responses;

public class QuizExamResponseModel
{
    public string TotalScore { get; set; }

    public IEnumerable<QuestionExamResponseModel> Questions { get; set; }
}
