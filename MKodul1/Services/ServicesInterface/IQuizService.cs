using MKodul1.Entity;

namespace MKodul1.Services.ServicesInterface
{
    public interface IQuizService
    {
        Task<List<QuizAnswer>> GenerateQuiz(QuizRequest request);
        Task<bool> CheckFromDatabase(Guid questionId, string selectedAnswer);
    }
}
