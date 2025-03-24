using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TucGolfklubb.DataContext.SqlServer
{
    public static class TucContextExtensions
    {
        public static IServiceCollection AddTucGolfDatabase(
            this IServiceCollection services, string? connectionString = null)
        {
            if (connectionString is null)
            {
                SqlConnectionStringBuilder builder = new();

                builder.DataSource = "(localdb)\\MSSQLLocalDB";
                builder.InitialCatalog = "TucGolfDatabase";
                builder.TrustServerCertificate = true;
                builder.MultipleActiveResultSets = true;

                builder.ConnectTimeout = 3;

                builder.IntegratedSecurity = true;

                connectionString = builder.ConnectionString;
            }
            services.AddDbContext<TucGolfDatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);

                options.LogTo(TucGolfContextLogger.WriteLine,
                    new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
            },

            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);

            return services;
        }
    }
}
