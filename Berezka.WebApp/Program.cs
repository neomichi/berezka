using System.IO;
using System.Reflection;
using System.Security.Authentication;
using System.Xml;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Berezka.WebApp
{
    public class Program
    {
   //     private static readonly log4net.ILog log
   //= log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static void Main(string[] args)
            {
            //XmlConfigurator.Configure(new FileInfo("..\\..\\log4net.config"));
            CreateHostBuilder(args).Build().Run();
            }

            public static IHostBuilder CreateHostBuilder(string[] args) => 
                    Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseKestrel(ept =>
                        {
                            ept.ConfigureHttpsDefaults(x =>
                            {
                                x.SslProtocols = SslProtocols.Tls12;
                            });
                        });

                        webBuilder.UseStartup<Startup>();
                    }).ConfigureLogging(builder =>
                    {
                        builder.SetMinimumLevel(LogLevel.Trace);
                        builder.AddLog4Net("log4net.config");
                        
                    });
        }
    }
   

