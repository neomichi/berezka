using System;
using Berezka.Data.Model;
using Berezka.Data.ViewModel;

namespace Berezka.Data.Service
{
    public interface ITokenService
    {
        TokenResult CheckingActionToken(string actionToken);
        TokenResult CheckingRefreshToken(string refreshToken);
        JsonWebToken CreateToken(Guid accountId);
    }
}