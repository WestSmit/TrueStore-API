using Microsoft.Extensions.DependencyInjection;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using DAL.Repositories.Interfaces;
using DAL.Repositories;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Initializer
{
    public static class DBInitializer
    {
        public static void DBInit(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<User, Role>(o=>
            {
                o.Password.RequireDigit = false;
                o.Password.RequiredLength = 6;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ProductContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork,EFUnitOfWork>();
        }
    }
}
