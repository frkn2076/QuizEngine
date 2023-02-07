using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Engine.Extensions;
using Quiz.Engine.Service.Contracts;
using Quiz.Engine.Service.DTOs;
using Quiz.Engine.ViewModels.Requests;
using Quiz.Engine.ViewModels.Responses;

namespace Quiz.Engine.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class QuizController : ControllerBase
{
    private readonly IQuizManagerService _quizManagerService;

    public QuizController(IQuizManagerService quizManagerService)
    {
        _quizManagerService = quizManagerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetQuizAsync(Guid quizId)
    {
        var userId = HttpContext.User.GetUserId();
        var quiz = await _quizManagerService.GetQuizAsync(quizId, userId);
        var response = quiz.Adapt<QuizDetailResponseModel>();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuizAsync(QuizDetailRequestModel request)
    {
        var userId = HttpContext.User.GetUserId();
        var quizDTO = request.Adapt<QuizDTO>();
        var quizId = await _quizManagerService.CreateQuizAsync(quizDTO, userId);
        var response = new QuizDetailSummaryResponseModel(quizId);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateQuizAsync(Guid quizId, QuizDetailRequestModel request)
    {
        var userId = HttpContext.User.GetUserId();
        var quizDTO = request.Adapt<QuizDTO>();
        await _quizManagerService.UpdateQuizAsync(quizDTO, quizId, userId);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteQuizAsync(Guid quizId)
    {
        var userId = HttpContext.User.GetUserId();
        await _quizManagerService.DeleteQuizAsync(quizId, userId);
        return Ok();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetOtherQuizzesAsync()
    {
        var userId = HttpContext.User.GetUserId();
        var quizzes = await _quizManagerService.GetOtherUserQuizzesAsync(userId);
        var response = quizzes.Adapt<QuizSummaryResponseModel[]>();
        return Ok(response);
    }

    [HttpPost("solve")]
    public async Task<IActionResult> SolveQuizAsync(QuizExamRequestModel request)
    {
        var userId = HttpContext.User.GetUserId();
        var quizExamDTO = request.Adapt<QuizExamDTO>();
        var response = await _quizManagerService.SolveQuizAsync(quizExamDTO, userId);
        return Ok(response);
    }

    [HttpGet("solutions")]
    public async Task<IActionResult> GetSolutionsOfQuizAsync(Guid quizId)
    {
        var userId = HttpContext.User.GetUserId();
        var solutionDetail = await _quizManagerService.GetSolutionsOfQuizAsync(quizId, userId);
        var response = solutionDetail.Adapt<QuizSolutionResponseModel>();
        return Ok(response);
    }

    [HttpGet("user/solutions")]
    public async Task<IActionResult> GetSolutionsOfUserAsync()
    {
        var userId = HttpContext.User.GetUserId();
        var solutionDetail = await _quizManagerService.GetSolutionsOfUserAsync(userId);
        var response = solutionDetail.Adapt<SolutionDetailResponseModel[]>();
        return Ok(response);
    }

    [HttpGet("user/all")]
    public async Task<IActionResult> GetQuizzesOfUserAsync()
    {
        var userId = HttpContext.User.GetUserId();
        var quizzes = await _quizManagerService.GetQuizzesOfUserAsync(userId);
        var response = quizzes.Adapt<QuizSummaryDetailResponseModel[]>();
        return Ok(response);
    }
}