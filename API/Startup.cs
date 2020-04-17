using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BLL.Initializer;
using BLL.Services.Interfaces;
using BLL.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BLL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using System.IO;

namespace API
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
            services.AddControllers();

            var path = Directory.GetCurrentDirectory();

            //string connectionString = @"Server=(localdb)\mssqllocaldb;Database=productsdb;Trusted_Connection=True;";
            string connectionString = $"Data Source=(localdb)\\mssqllocaldb;AttachDbFilename='{path}\\App_Data\\AppDB.mdf';Database=AppDB;Trusted_Connection=True;";

            services.DBInit(connectionString);
            services.AddCors();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });


            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();

            services.AddTransient<IProductService, ProductServices>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(options =>
                     {
                         options.RequireHttpsMetadata = false;
                         options.TokenValidationParameters = new TokenValidationParameters
                         {                            
                            ValidateIssuer = true,
                            ValidIssuer = AuthenticationOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthenticationOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthenticationOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true
                            
                         };
                         options.Events = new JwtBearerEvents
                         {
                             OnAuthenticationFailed = context =>
                             {

                                 if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                 {

                                     context.Response.Headers.Add("Token-Expired", "true");
                                 }
                                 return Task.CompletedTask;
                             }

                         };
                         
                     });
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:4200").WithOrigins("http://192.168.0.102:4200").AllowAnyHeader().AllowCredentials();
            });

            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
