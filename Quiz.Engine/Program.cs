using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Quiz.Engine.Data;
using Quiz.Engine.Data.Repositories.Contracts;
using Quiz.Engine.Data.Repositories.Implementations;
using Quiz.Engine.Extensions;
using Quiz.Engine.Filters;
using Quiz.Engine.Mapper;
using Quiz.Engine.Middleware;
using Quiz.Engine.Service;
using Quiz.Engine.Service.Contracts;
using Quiz.Engine.Service.Implementations;
using Quiz.Engine.Utils.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options => options.Filters.Add(typeof(ValidateModelStateAttribute)))
    .AddFluentValidation(s => s.RegisterValidatorsFromAssemblyContaining<Program>());

ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services
    .Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQuizEngineRepository, QuizEngineRepository>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IQuizManagerService, QuizManagerService>();

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection(nameof(JWTSettings)));

var jwtSettings = builder.Configuration.GetOptions<JWTSettings>();
builder.Services.RegisterJWTAuthentication(jwtSettings);

var serviceProvider = builder.Services.BuildServiceProvider();
var quizEngineRepository = serviceProvider.GetRequiredService<IQuizEngineRepository>();

Feeder.CreateSchemas(quizEngineRepository);
Feeder.FillTemporaryData(quizEngineRepository);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is CustomeException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(exception.Message);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync("Something went wrong");
        }
    });
});

ObjectMapper.Init();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<AuthenticationMiddleware>();

app.MapControllers();

app.Run();