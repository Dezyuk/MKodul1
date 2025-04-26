using Microsoft.EntityFrameworkCore;
using MKodul1.Context;
using MKodul1.Entity;
using MKodul1.Exceptions;
using MKodul1.Services.ServicesInterface;
using System.Linq;

namespace MKodul1.Services
{
    public class QuizService : IQuizService
    {
        private readonly QuizDBContext _context;

        public QuizService(QuizDBContext context)
        {
            _context = context;
        }

        public async Task<List<QuizAnswer>> GenerateQuiz(QuizRequest request)
        {
            if (request.CategoryId == Guid.Empty)
            {
                throw new InvalidQuizRequestException(nameof(request.CategoryId), "Id категории не может быть пустым.");
            }

            if (request.Count <= 0)
            {
                throw new InvalidQuizRequestException(nameof(request.Count), "Количество вопросов должно быть больше нуля.");
            }

            var questions = await _context.Questions
                .Where(q => q.Categories.Any(c => c.Id == request.CategoryId))
                .Include(q => q.Answer)
                .OrderBy(q => Guid.NewGuid())
                .Take(request.Count)
                .ToListAsync();

            if (!questions.Any())
            {
                throw new CategoryNotFoundException(nameof(request.CategoryId), "Категория не найдена.");
            }


            var result = new List<QuizAnswer>();

            foreach (var question in questions)
            {
                var correct = question.Answer.Title;
                var incorrect = await _context.Answers
                    .Where(a => a.Title != correct)
                    .OrderBy(a => Guid.NewGuid())
                    .Select(a => a.Title)
                    .Take(1)// на каждый вопрос по 1 неправильному ответу
                    .ToListAsync();

                var mixedAnswers = incorrect.Append(correct).OrderBy(a => Guid.NewGuid()).ToList();



                result.Add(new QuizAnswer
                {
                    QuestionId = question.Id,
                    Title = question.Title,
                    Answers = mixedAnswers
                });
            }

            return result;
        }

        
        

        public async Task<bool> CheckFromDatabase(Guid questionId, string selectedAnswer)
        {
            var question = await _context.Questions
                .Include(q => q.Answer)
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question == null)
            {
                throw new QuestionNotFoundException(nameof(questionId), "Вопрос не найден.");
            }
            
            return question.Answer.Title.Trim().ToLower() == selectedAnswer.Trim().ToLower();
        }
    }
}

