using System;
using Berezka.Data.Service;
using Berezka.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Berezka.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class FaqController : ControllerBase
    {
        readonly private IFaqService _faqService;
        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public IActionResult GetAll()
        {
            return Ok(_faqService.GetAllFaq());
        }
        [ResponseCache(Duration = 30)]

        [HttpGet("answer")]
        public IActionResult GetCategorise(Guid Id)
        {
            return Ok(_faqService.GetAnswer(Id));

        }

        [ResponseCache(Duration = 30)]
        [HttpGet("categories")]
        public IActionResult GetCategorise()
        {
            var gg = _faqService.GetAllCategories();
            return Ok(gg);
        }
        [ResponseCache(Duration = 30)]
        [HttpPost]
        public IActionResult Add([FromBody]FaqView fv)
        {
             _faqService.AddQuestionAnswers(fv);
            return Ok();
        }

    }
}
