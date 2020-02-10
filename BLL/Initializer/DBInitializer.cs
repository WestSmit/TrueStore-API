using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using DAL.Repositories;

namespace BLL.Initializer
{
    public static class DBInitializer
    {
        public static void DBInit(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork,EFUnitOfWork>();
        }
    }
}
