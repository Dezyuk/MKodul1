using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKodul1.Entity;
using MKodul1.Services.ServicesInterface;

namespace MKodul1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : Controller
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Generate(QuizRequest request)
        {
            var ansver = await _quizService.GenerateQuiz(request);
            return Ok(ansver);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CheckAnswer(Guid questionId, string selectedAnswer)
        {
            var result = await _quizService.CheckFromDatabase(questionId, selectedAnswer);
            return result == true ? Ok("Your answer is correct.") : Ok("Your answer is incorrect.");
        }
    }
}
