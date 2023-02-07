using Mapster;
using Quiz.Engine.Data.Entities;
using Quiz.Engine.Data.Enums;
using Quiz.Engine.Data.Models;
using Quiz.Engine.Helper;
using Quiz.Engine.Service.DTOs;
using Quiz.Engine.ViewModels.Requests;
using Quiz.Engine.ViewModels.Responses;

namespace Quiz.Engine.Mapper;

public class ObjectMapper
{
    public static void Init()
    {
        #region RequestToDTO

        TypeAdapterConfig<UserRequestModel, UserDTO>.NewConfig();

        TypeAdapterConfig<QuizDetailRequestModel, QuizDTO>.NewConfig();

        #endregion

        #region DTOToEntity

        TypeAdapterConfig<UserDTO, User>.NewConfig()
            .Map(dest => dest.Password, src => CryptoHelper.EncryptPassword(src.Password));

        TypeAdapterConfig<QuizDTO, Data.Entities.Quiz>.NewConfig();

        TypeAdapterConfig<QuestionDTO, Question>.NewConfig()
            .Map(dest => dest.Type, src => src.Answers.Count(x => x.IsCorrectAnswer) == 1 ? QuestionType.Single : QuestionType.Multiple)
            .Map(dest => dest.Text, src => src.Question);

        TypeAdapterConfig<AnswerDTO, Answer>.NewConfig()
            .Map(dest => dest.Text, src => src.Answer);

        #endregion

        #region EntityToDTO

        TypeAdapterConfig<Data.Entities.Quiz, QuizDTO>.NewConfig();

        TypeAdapterConfig<QuizSummary, QuizSummaryDTO>.NewConfig();

        #endregion

        #region DTOToResponse

        TypeAdapterConfig<QuizSummaryDTO, QuizSummaryResponseModel>.NewConfig();

        TypeAdapterConfig<QuizDTO, QuizDetailResponseModel>.NewConfig();

        TypeAdapterConfig<QuestionDTO, QuestionDetailResponseModel>.NewConfig()
            .Map(dest => dest.IsSingleChoice, src => src.Answers.Count(x => x.IsCorrectAnswer) == 1);

        TypeAdapterConfig<SolutionDetailDTO, SolutionDetailResponseModel>.NewConfig();

        TypeAdapterConfig<QuizSolutionDTO, QuizSolutionResponseModel>.NewConfig();

        #endregion
    }
}
