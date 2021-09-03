using System.Net;
using Berezka.Data.EnumType;
using Berezka.Data.Model;
using Berezka.Data.Service;
using Berezka.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Berezka.WebApp.Controllers
{
    [Route("api/[controller]")]   
    public class TokenController : Controller
    {  


        private IAccountService _accountService;
        private readonly ITokenService _tokenService;
        public TokenController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }
           
        //[HttpPost]
        //public IActionResult GetToken([FromBody] AccountView accountView) 
        //{
        //    if (!ModelState.IsValid) return NotFound(); 

        //    var account = _accountService.GetAccount(accountView);
        //    if (account == null) return Ok();
        //    var accountId = _tokenService.CreateToken(account.Id); return Ok(accountId);             

        //} 
        
        [HttpPost("refreshToken")]
        public IActionResult RefreshToken([FromBody] RefreshTokenView refreshToken)
        {            
            if (string.IsNullOrWhiteSpace(refreshToken.RefreshToken) ) return BadRequest();

             var result = _tokenService.CheckingRefreshToken(refreshToken.RefreshToken);

            if (result.ReturnType == TokenState.Ok) {

                return Ok(_tokenService.CreateToken(result.Value));
            }
            
            return StatusCode(403);

           


        }

        
    }
}
