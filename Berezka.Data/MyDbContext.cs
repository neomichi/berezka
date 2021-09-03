using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Berezka.Data.EnumType;
using Berezka.Data.Model;
using Berezka.Data.ViewModel;
using System.Linq;

using log4net;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Berezka.Data
{
 


    public class MyDbContext : DbContext
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DbContext));

        public MyDbContext(DbContextOptions<MyDbContext> options)
           : base(options)
        {
            
     
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<FaqCategory> FaqCategories { get; set; }
        public DbSet<FaqQuestionAnswer> FaqQuestionAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
       


            builder.HasPostgresExtension("uuid-ossp");
            builder.Entity<Account>().HasIndex(x => x.Email).IsUnique();
            builder.Entity<Role>().HasIndex(x => x.Title).IsUnique();
            builder.Entity<AccountRole>().HasIndex(p => new { p.AccountId, p.RoleId }).IsUnique();


            #region obsolete
            //var adminRole = new Role() { Id = Guid.NewGuid(), Title = "admin" };
            //var managerRole = new Role() { Id = Guid.NewGuid(), Title = "manager" };
            //var userRole = new Role() { Id = Guid.NewGuid(), Title = "user" };


            //var admin = new Account() {Id=Guid.NewGuid(), Fio = "ivan one", Email = "admin@test.ru", SetPassword = "LikeMe123!" };
            //var manager = new Account() { Id = Guid.NewGuid(), Fio = "ivan two", Email = "manager@test.ru", SetPassword = "Cx123@" };
            //var user = new Account() { Id = Guid.NewGuid(), Fio = "ivan three", Email = "user@test.ru", SetPassword = "Qw!xcdw1" };

            //builder.Entity<Role>().HasData(adminRole);
            //builder.Entity<Role>().HasData(managerRole);
            //builder.Entity<Role>().HasData(userRole);


            //builder.Entity<Account>().HasData(admin);
            //builder.Entity<Account>().HasData(manager);
            //builder.Entity<Account>().HasData(user);


            //builder.Entity<AccountRole>().HasData(new AccountRole { Account = admin, Role = adminRole });
            //builder.Entity<AccountRole>().HasData(new AccountRole { Account = manager, Role = managerRole });
            //builder.Entity<AccountRole>().HasData(new AccountRole { Account = user, Role = userRole });





            //builder.Entity<Account>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
            //builder.Entity<Role>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
            //builder.Entity<AccountRole>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
            //builder.Entity<Message>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
            //builder.Entity<FaqCategory>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
            //builder.Entity<FaqQuestionAnswer>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");

            //typeof(Entity).GetProperty("Id")
            //()=>этим можно пользоваться<=()//

            //builder.Entity<Role>().HasData(roles);
            // builder.Entity<Account>().HasData(accounts);
            #endregion
            base.OnModelCreating(builder);
        }
        protected DbContextOptions<MyDbContext> ContextOptions { get; }
        //[DbFunction("SOUNDEX")]
        //public static string Soundex(string s) => throw new Exception();

        private void SaveInLog()
        {
            var entities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added ||
            x.State == EntityState.Modified).ToArray();               

            //самый быстрый на диком западе  for(var i=0;i<entities.Length;i++) {}
            foreach(var entity in entities)
            {                
              Log.Warn($"EF=> {entity}");                
            }            
        }

        public override int SaveChanges()
        {
            SaveInLog();            
            return base.SaveChanges();
        }


    }

    
}

