using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Berezka.Data.Service;
using Berezka.Data.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace TestWebApp.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    // [CustomCheckingTokenAuthorize]
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        private ITokenService _tokenService;

        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ITokenService tokenService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _logger = logger;

        }

        /// <summary>
        ///  Get Account ByToken
        /// </summary>
        /// <returns>Account</returns>
        [HttpGet]
        public IActionResult Index()
        {            
            var guid = Guid.Empty;             
            if (HttpContext.Request.Headers.Keys.Contains("Authorization") &&
                    !string.IsNullOrWhiteSpace(HttpContext.Request.Headers["Authorization"]))
            {
                var actionToken = HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length); 
                guid = _tokenService.CheckingActionToken(actionToken).Value;
            }
            
            if (guid == Guid.Empty) return BadRequest("badtoken");          
            
            return  Ok( _accountService.GetAccount(guid));
        }


        [AllowAnonymous]
        [HttpPost]
        public  ActionResult<AccountView> Register([FromBody] AccountView accountView)
        { 
            if (!ModelState.IsValid)
            { 
                return Ok();
            }
            _logger.LogTrace($"Account Register {accountView}");    
            
            var resp = _accountService.CreateOrEditAccount(accountView);
            if (resp != null) return Ok(resp); else return Ok();
        }

        [AllowAnonymous]
        [HttpPut]
        public  IActionResult Login([FromBody] AccountLoginView accountView)
        {
    
            if (!ModelState.IsValid) return BadRequest();
            _logger.LogTrace($"Account Login {accountView}");
            var account =  _accountService.GetAccount(accountView);
                if (account == null) return BadRequest();

                var accountId = _tokenService.CreateToken(account.Id); return Ok(accountId);
        }


        [ResponseCache(Duration = 1)]
        [AllowAnonymous]
        [HttpPost("freeemail")]
        public async Task<IActionResult> FreeEmail([FromBody] AccoutEmail email)
        {
            if (email == null) return BadRequest(false);

            _logger.LogWarning($"Account EmailFree {email.email}");

            return await _accountService.EmailFree(email.email) ? Ok(false) : BadRequest(); ;
        }

        [ResponseCache(Duration = 1)]
        [AllowAnonymous]
        [HttpPost("freeurl")]
        public async Task<IActionResult> FreeUrl([FromBody] AccoutUrl url)
        {
            _logger.LogWarning($"Accout Url! {url.url}");
            return await _accountService.UrlFree(url.url)? Ok(true) :BadRequest(); 
        }

    }


}
