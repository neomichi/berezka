using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Berezka.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var baseName = "Berezka";
            var projectName = "Berezka.WebApp";

             

            var index = Environment.CurrentDirectory.IndexOf(baseName);
            var basePath = Environment.CurrentDirectory.Substring(0, index + baseName.Length);
            var appsettingsPath = Path.Combine(basePath, projectName, "appsettings.json");

            var json = File.ReadAllText(appsettingsPath);
            var connectionString = JObject.Parse(json)["ConnectionStrings"]["PostgreSQL"].ToString();
          

          //  IConfigurationRoot configuration = new ConfigurationBuilder().Build();

                

            var builder = new DbContextOptionsBuilder<MyDbContext>();
            

            builder.UseNpgsql(connectionString);
          
            return new MyDbContext(builder.Options);
        }
    }
}
