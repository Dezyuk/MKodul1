using MKodul1.Entity;

namespace MKodul1.Services.ServicesInterface
{
    public interface IQuestionsService
    {
        Task AddNewQuestion(AddNewQuestionRequest request);
    }
}
