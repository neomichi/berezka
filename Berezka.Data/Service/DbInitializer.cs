using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Berezka.Data.Model;

namespace Berezka.Data.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MyDbContext _db;


       // readonly ILogger<DbInitializer> _logger;


        //public UserStore<ApplicationUser> User { get; set; }

        public DbInitializer(MyDbContext db,
            ILogger<DbInitializer> logger)
        {
            _db = db;
           
        }


        public  async Task Initialize()
        {


        


            

            await _db.Database.MigrateAsync();


            if (await _db.Accounts.AnyAsync()) return;


            using (var transact = await _db.Database.BeginTransactionAsync())
            {


                try
                {

                    var adminRole = new Role() { Title = "admin" };
                    var managerRole = new Role() { Title = "manager" };
                    var userRole = new Role() { Title = "user" };


                    var admin = new Account() { Fio = "ivan one", Email = "admin@test.ru", SetPassword = "LikeMe123!" };
                    var manager = new Account() { Fio = "ivan two", Email = "manager@test.ru", SetPassword = "Cx123@" };
                    var user = new Account() { Fio = "ivan three", Email = "user@test.ru", SetPassword = "Qw!xcdw1" };


                    await _db.Roles.AddRangeAsync(new Role[] { adminRole, managerRole, userRole });
                    await _db.Accounts.AddRangeAsync(new Account[] { admin, manager, user });


                    await _db.AccountRoles.AddRangeAsync(
                        new AccountRole { Account = admin, Role = adminRole },
                        new AccountRole { Account = manager, Role = managerRole },
                        new AccountRole { Account = user, Role = userRole }
                        );

                    var faqCategory1 = new FaqCategory() { Category = "обычная" };
                    var faqCategory2 = new FaqCategory() { Category = "прикольна" };
                    await _db.FaqCategories.AddAsync(faqCategory1);
                    await _db.FaqCategories.AddAsync(faqCategory2);

                    await _db.SaveChangesAsync();
                    await transact.CommitAsync();
                }
                catch
                {
                    await transact.RollbackAsync();
                }


                
            }

        }
    }
}
