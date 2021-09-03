using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Berezka.Data.ViewModel;

namespace Berezka.Data.Service
{
    public interface IAccountService
    {
        AccountView CreateOrEditAccount(AccountView accountView);
        ValueTask<bool> EmailFree(string email);
        ValueTask<bool> ExistAccount(AccountView accountView);
        AccountView GetAccount(AccountLoginView accountView);
        AccountView GetAccount(Guid accountId);
        List<AccountView> GetAllAccount();
        ValueTask<bool> UrlFree(string url);
    }
}