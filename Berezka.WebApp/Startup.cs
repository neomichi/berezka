using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Berezka.Data;
using Berezka.Data.Service;
using Berezka.WebApp.Service;
using log4net;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


namespace Berezka.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("PostgreSQL");


            services.AddEntityFrameworkNpgsql()
               .AddDbContext<MyDbContext>(options =>
                  options.UseNpgsql(connectionString)
            );


            //services.AddSingleton<MyDbContext, MyDbContext>();
            //services.AddSingleton(typeof(AxiosView<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddSingleton<ICryptoHelper, RsaHelper>();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowAnyHeader());
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                
            });

            var bearerConfig = Configuration.GetSection("Jwt");

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IFaqService, FaqService>();
          
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                .AddJwtBearer(cfg =>
                {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = false;

                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                       
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bearerConfig["SecurityKey"])),
                        ValidIssuer = bearerConfig["Issuer"],
                        ValidAudience = bearerConfig["Audience"],
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        // ValidateLifetime = false,
                      // ValidateLifetime = false,                     
                       // LifetimeValidator = TokenService.CustomLifetimeValidator
                        
                    };

                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = false;
                    cfg.Events = new JwtBearerEvents
                    {                    
                    
                        //OnAuthenticationFailed = ctx =>
                        //{
                        //   // cfg.TokenValidationParameters.ClockSkew.
                        //    if (ctx.Exception.GetType()==typeof(SecurityTokenValidationException))
                        //    {
                               
                        //        ctx.Response.Headers.Add("Token-Expired1", "true");
                        //    }

                        //  var gg=  cfg.TokenValidationParameters.ClockSkew;
                        //    var zz = DateTime.UtcNow;
                           
                        //    // var op = context.Options;//
                        //    if (ctx.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        //    {
                        //        ctx.Response.Headers.Add("Token-Expired", "true");

                        //    }
                        //    return Task.CompletedTask;
                        //},

                        //OnChallenge = ctx =>
                        //{
                        //    var value = ctx.HttpContext.Request.Headers.Keys.Contains("Authorization");
                        //    if (!value)
                        //    {
                        //        ctx.Response.Headers.Add("reponse", "Token Not Found");
                        //        ctx.Response.StatusCode = 404;
                        //        return ctx.Response.WriteAsync("Token Not Found");
                        //    }
                        //    ctx.HandleResponse();
                        //    ctx.Response.StatusCode = 301;

                        //    return ctx.Response.WriteAsync("The token has expired,  refresh token");
                        //},

                    };
                });
            ///seo //is not work :-'
            services.AddRouting(cfg =>
            {
                cfg.LowercaseQueryStrings = true;
                cfg.LowercaseUrls = true;
                
            });
            services.Configure<RouteOptions>(options => options.AppendTrailingSlash = false);

            ////end
        
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;  
                });
  
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1", });
                
            });     
            services.AddResponseCaching();
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
          IDbInitializer dbInitializer )
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(o =>{
            });
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));

            //app.UseSwagger(o =>
            //{
            //    o.SerializeAsV2 = true;
            //});
            //app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "webapi"));



            app.UseHsts();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseResponseCaching();
            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseAuthorization();

            //var supportedCultures = new[]
            //{
            //    new CultureInfo("en"),
            //    new CultureInfo("ru"),
            //    new CultureInfo("de")
            //};
            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("ru"),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures
            //});


            //  app.AddDistributedTokenCaches();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
     
            dbInitializer.Initialize().Wait();


        }

       }
}
