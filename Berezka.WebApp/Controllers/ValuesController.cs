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
        private readonly MyDbContext _db;


      
        public ValuesController(MyDbContext db)
        {
            _db = db;
        }

        [SwaggerOperation(
          Summary = "Run Test",
         Description = "Allow any",    
    Tags = new[] { "TestApi" }
        )]

      


        [HttpGet]
        public IActionResult Get()
        {
            

          var name = "not auth";

            if (User.Identity.IsAuthenticated)
            {
                name = User.Identity.Name;
                var claims = User.GetClaims();
            }
          
            log.Error("rabotaet");
            log.Error("работает");
            
            return Ok(name);
        }
    }
}
