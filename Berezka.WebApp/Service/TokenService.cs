using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Berezka.Data.Service;
using Berezka.Data;
using Berezka.Data.ViewModel;
using Berezka.Data.EnumType;
using Berezka.Data.Model;

namespace Berezka.WebApp.Service
{
    public class TokenService : ITokenService
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly string Issuer = "";
        private readonly string Audience = "";
        private readonly int ExpiresMinutes = 0;
        private readonly int RefreshTokenMinutes = 0;
        private readonly string SecurityKey = "";      
        private ICryptoHelper _cryptoHelper;

        public TokenService(IAccountService accountService, IConfiguration configuration, ICryptoHelper cryptoHelper)
        {
            _accountService = accountService;
            _configuration = configuration.GetSection("Jwt");
            _cryptoHelper = cryptoHelper;

            Issuer = _configuration["Issuer"];
            Audience = _configuration["Audience"];
            ExpiresMinutes = _configuration["TokenExpiresMinutes"].ToInt();
            RefreshTokenMinutes = _configuration["RefreshTokenMinutes"].ToInt();
            SecurityKey = _configuration["SecurityKey"];
        }


        public static bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                return DateTime.UtcNow < expires;
            }
            return false;
        }

        public TokenResult CheckingActionToken(string actionToken)
        {
            var tokenResult = new TokenResult(Guid.Empty);
            JwtSecurityToken securityToken = new JwtSecurityTokenHandler().ReadJwtToken(actionToken);

            var stringId = securityToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            tokenResult.Value = stringId.ToGuid();
            tokenResult.ReturnType = Audience.EQ(securityToken.Audiences.First()) &&
            Issuer.EQ(securityToken.Issuer) && DateTime.UtcNow < securityToken.ValidTo
            ? TokenState.Ok : TokenState.Bad;
            return tokenResult;
        }
        /// <summary>

        /// </summary>
        /// <param name="actionToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public TokenResult CheckingRefreshToken(string refreshToken)
        {
            var tokenResult = new TokenResult(Guid.Empty);

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                
                try
                {
                    var decryptData = _cryptoHelper.DecryptData(refreshToken);
                    tokenResult.Value = decryptData.UserId;
                    tokenResult.ReturnType = DateTime.UtcNow < decryptData.fulldatetime && SecurityKey.EQ(decryptData.securityKey)
                        ? TokenState.Ok : TokenState.Bad;
                } 
                catch
                {
                    tokenResult.ReturnType = TokenState.Bad;
                    
                }


              
            }
            return tokenResult;
        }

        public JsonWebToken CreateToken(Guid accountId)
        {
            //Add Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,accountId.ToString("N")),
                new Claim(JwtRegisteredClaimNames.NameId, accountId.ToString("N")),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var refreshTokenDate = DateTime.UtcNow.AddMinutes(RefreshTokenMinutes).FullToString();
            var refreshtoken = _cryptoHelper.EncryptData(accountId, refreshTokenDate, SecurityKey);

            var token = new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(ExpiresMinutes),
                
                signingCredentials: creds);
      
        
            return
                new JsonWebToken(
                    accessToken: new JwtSecurityTokenHandler().WriteToken(token),
                    expiresin: ExpiresMinutes*60,
                    
                    refreshToken: refreshtoken);

                
        }
    }
}
