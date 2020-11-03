using Interesse.DbLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interesse.DbLayer.Configuration
{
    public class DbConfig
    {
        static string _connectionString;

        public static string ConnectionString => _connectionString;

        public static void ConfigureDb(IServiceCollection services, string connectionString)
        {
            _connectionString = connectionString;

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Interesse.App")));
        }
    }
}
