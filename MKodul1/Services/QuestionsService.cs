using Microsoft.EntityFrameworkCore;
using MKodul1.Context;
using MKodul1.Entity;
using MKodul1.Exceptions;
using MKodul1.Services.ServicesInterface;

namespace MKodul1.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly QuizDBContext _context;

        public QuestionsService(QuizDBContext context)
        {
            _context = context;
        }

        public async Task AddNewQuestion(AddNewQuestionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || request.Title.Length < 5 || request.Title.Length > 250)
            {
                throw new QuestionValidationException(nameof(request.Title), "Название вопроса не должно быть пустым");
            }
            if (string.IsNullOrWhiteSpace(request.Answer) || request.Answer.Length < 5 || request.Answer.Length > 80)
            {
                throw new QuestionValidationException(nameof(request.Answer), "Текст ответа не должен быть пустым");
            }
            if (request.Categories is null || !request.Categories.Any())
            {
                throw new QuestionValidationException(nameof(request.Categories), "У вопроса должна быть хотя бы одна категория");
            }

            var categoriesInDb = await _context.Categories
                .Where(c => request.Categories.Contains(c.Id))
                .ToListAsync();

            if (request.Categories.Count != categoriesInDb.Count)
            {
                var foundIds = categoriesInDb.Select(c => c.Id);
                var missingIds = request.Categories.Except(foundIds);
                throw new CategoryNotFoundException(nameof(request.Categories), $"Категории с Id не найдены: {string.Join(", ", missingIds)}");
            }

            var answer = new Answer
            {
                Id = Guid.NewGuid(),
                Title = request.Answer
            };


            var question = new Question
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Answer = answer,
                Categories = categoriesInDb
            };
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
        }
    }
}
