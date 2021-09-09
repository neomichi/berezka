using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Berezka.Data;
using log4net;
using log4net.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace Berezka.WebApp.Controllers
{
    /// <summary>
    /// test
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ValuesController));
                    
    

        [SwaggerOperation(
          Summary = "Run Test",
          Description = "Allow any",    
          Tags = new[] { "TestApi" }
        )]      


        [HttpGet]
        public IActionResult Get()
        {        

           
            log.Info("работает");
            
            return Ok("is work?");
        }
    }
}
