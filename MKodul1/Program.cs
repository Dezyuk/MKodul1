using Microsoft.EntityFrameworkCore;
using MKodul1.Context;
using MKodul1.ExceptionHandlers;
using MKodul1.Services;
using MKodul1.Services.ServicesInterface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<QuizDBContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DB"));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IQuestionsService, QuestionsService>();
builder.Services.AddTransient<IQuizService, QuizService>();



builder.Services.AddExceptionHandler<CategoryExceptionHandler>();
builder.Services.AddExceptionHandler<CategoryValidationExceptionHandler>();

builder.Services.AddExceptionHandler<QuestionValidationExceptionHandler>();
builder.Services.AddExceptionHandler<CategoryNotFoundExceptionHandler>();

builder.Services.AddExceptionHandler<InvalidQuizRequestExceptionHandler>();
builder.Services.AddExceptionHandler<QuestionNotFoundExceptionHandler>();

builder.Services.AddExceptionHandler<ServerExceptionsHandler>();

builder.Services.AddProblemDetails();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
