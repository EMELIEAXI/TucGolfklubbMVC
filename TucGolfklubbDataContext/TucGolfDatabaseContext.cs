using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace TucGolfklubb.DataContext.SqlServer
{
    public partial class TucGolfDatabaseContext : DbContext
    {
        public TucGolfDatabaseContext()
        {
        }

        public TucGolfDatabaseContext(DbContextOptions<TucGolfDatabaseContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                SqlConnectionStringBuilder builder = new();

                builder.DataSource = "(localdb)\\MSSQLLocalDB";
                builder.InitialCatalog = "TucGolfDatabase";
                builder.TrustServerCertificate = true;
                builder.MultipleActiveResultSets = true;

                builder.ConnectTimeout = 3;

                builder.IntegratedSecurity = true;

                optionsBuilder.UseSqlServer(builder.ConnectionString);

                optionsBuilder.LogTo(TucGolfContextLogger.WriteLine,
                new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
            }
        }
    }

}