using System;
using Berezka.Data.Model;
using Berezka.Data.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Berezka.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageSevice;
        public  MessageController(IMessageService messageSevice)
        {
            _messageSevice = messageSevice;
        }

       
        [HttpGet("GetMessages")]
        public ActionResult GetMessages(Guid AccountId)
        {
           return Ok(_messageSevice.GetFromGuid(AccountId));

        }
        [HttpGet("GetCountMessages")]
        public ActionResult GetCountMessages(Guid AccountId)
        {
         
            return Ok(_messageSevice.GetCountMessages(AccountId));
        }
        [HttpPost]
        public ActionResult Post(Message message)
        {
            _messageSevice.AddMessage(message);

            return Ok("");
                
        }
        [HttpDelete]
        public ActionResult Delete(Message message)
        {
            ///_messageSevice.;
            ///
            

            return Ok("");

        }
    }
}
