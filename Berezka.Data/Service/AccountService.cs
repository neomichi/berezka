using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Berezka.Data.Model;
using Berezka.Data.ViewModel;
using Berezka.Data.EnumType;


namespace Berezka.Data.Service
{
    public class AccountService : IAccountService
    {
        private MyDbContext _db;

        public AccountService(MyDbContext db)
        {
            _db = db;
        }

        private IQueryable<Account> Accounts()
        {

            return _db.Accounts
                .Include(x => x.AccountRoles)
                .ThenInclude(y => y.Role)
                .Where(x => x.Visible);
        }

        private IQueryable<Account> AccountsFull()
        {
            return Accounts()
                .Include(x => x.AccountRoles)
                .ThenInclude(y => y.Role);

        }

        public async ValueTask<bool> EmailFree(string email)
        {
            return !string.IsNullOrWhiteSpace(email)
                   && email.Length > 3
                   && email.IndexOf("@") > 0
                   && !await Accounts().AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async ValueTask<bool> UrlFree(string url)
        {
            return (!string.IsNullOrWhiteSpace(url)
                    && Regex.IsMatch(url, "^[A-Za-zА-Яа-я0-9_-]{3,10}$"))
                    && !await Accounts().AnyAsync(x => x.Url.ToLower() == url.ToLower());
        }


        public AccountView GetAccount(Guid accountId)
        {
            return AccountToAccountView(AccountsFull().SingleOrDefault(x => x.Id == accountId));
        }

        public async ValueTask<bool> ExistAccount(AccountView accountView)
        {
            return await Accounts().SingleOrDefaultAsync(x =>

            x.Email.ToLower() == accountView.Email.ToLower() &&
            x.Password.ToLower() == accountView.Password) != null ? true : false;

        }

        public AccountView GetAccount(AccountLoginView accountLoginView)
        {
            var passwordHash = Helper.StrToHash(accountLoginView.Password);

            var account = Accounts().SingleOrDefault(x =>
               x.Email.ToLower() == accountLoginView.Email.ToLower() &&
               EF.Equals(x.Password, passwordHash)
            );

            if (account == null)
            {
                return null;
            }
            else
            {
                return AccountToAccountView(account);
            }
        }


        public List<AccountView> GetAllAccount()
        {
            return Accounts().ToArray().Select(x => AccountToAccountView(x)).ToList();
        }

        public AccountView CreateOrEditAccount(AccountView accountView)
        {

            Account account = AccountViewToAccount(accountView);


            if (accountView.Id == Guid.Empty)  _db.Add(account);

            else
            {

                account =  Accounts().SingleOrDefault(x => x.Id == accountView.Id);

                if (account == null) return null;

               

                var roles = account.AccountRoles.Where(x => x.AccountId == account.Id);

                account = AccountViewToAccount(accountView);

                _db.Accounts.Update(account);

            }
            if (accountView.Id == Guid.Empty)
            {

                var userRole = _db.Roles.First(x => EF.Functions.Like(x.Title, "user"));
                _db.AccountRoles.Add(new AccountRole() { Account = account, Role = userRole });
            }
            int change = _db.SaveChanges();
            return change == 1 ? AccountToAccountView(account) : null;

        }

        public Account AccountViewToAccount(AccountView accountView)
        {

            var account = new Account()
            {
                Id = accountView.Id,
                Fio = accountView.Fio,
                Url = accountView.Url,
                Email = accountView.Email,
                Avatar = accountView.Avatar,
            };

            if (!string.IsNullOrWhiteSpace(accountView.Password))
                account.SetPassword = accountView.Password;

            return account;
        }

        public AccountView AccountToAccountView(Account account)
        {
            var roles = new int[] { };


            if (account.AccountRoles != null)
            {

                var rolesEnumDic = Enum.GetValues(typeof(Roles)).Cast<Roles>()
                    .Select(x => ((int)x, x.ToString())).ToList();

                roles = account.AccountRoles.Select(x =>
                rolesEnumDic.FirstOrDefault(y => y.Item2.EQ(x.Role.Title)).Item1)
                    .ToArray();



                //new   { A =
                //rolesEnumDic.FirstOrDefault(y => y.Item2.EQ(x.Role.Title)).Item1
                //}
                //).ToList();

                //var bb = rolesDb;
                //for (int i = 0; i < rolesDb.Count; i++)
                //{
                //    roles[i] = (rolesEnumDic.SingleOrDefault(x => x.Item2.EQ(rolesDb[i].Title)).Item1);
                //}
            }

            var accountView = new AccountView()
            {
                Id = account.Id,
                Fio = account.Fio,
                Avatar = account.Avatar,
                Url = account.Url,
                Email = account.Email,
                Roles = roles[0] //костыль потом поменяю
            };
            return accountView;

        }

        //private ValueTask<List<int>> UserRole()
        //{
        //    int[] roles = Array.Empty<int>();

        //    var rolesEnumDic = Enum.GetValues(typeof(Roles)).Cast<Roles>()
        //        .Select(x => ((int)x, x.ToString()));

        //    var rolesDb = account.AccountRoles;
        //    var gg = rolesDb.Select(x => x.Role).ToList();

        //    for (int i = 0; i < rolesDb.Count; i++)
        //    {
        //        roles[i] = (rolesEnumDic.SingleOrDefault(x => x.Item2.EQ(gg[i].Title)).Item1);
        //    }

        //    return roles;
        //}


    }
}
