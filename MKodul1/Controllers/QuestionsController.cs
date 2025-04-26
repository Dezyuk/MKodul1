using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MKodul1.Context;
using MKodul1.Entity;
using MKodul1.Services.ServicesInterface;

namespace MKodul1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : Controller
    {
        private readonly IQuestionsService _questionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }



        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddNewQuestion(AddNewQuestionRequest request)
        {
            await _questionsService.AddNewQuestion(request);
            return NoContent();
        }
        
    }
}
