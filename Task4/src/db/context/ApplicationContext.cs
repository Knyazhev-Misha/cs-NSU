using Microsoft.EntityFrameworkCore;
 
namespace Task4
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Experiment> Experiments { get; set; }
        private static DbName dbName = DbName.PostgreSQL;
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public static void SetDb(DbName dbName)
        {
            ApplicationContext.dbName = dbName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (dbName == DbName.PostgreSQL)
            {
                 optionsBuilder.UseNpgsql("Host=localhost;Port=1111;Database=cs_bd;Username=postgres;Password=1111");
            }

            else if(dbName == DbName.LocalSQL)
            {
                 optionsBuilder.UseInMemoryDatabase("MyDatabase"); 
            }
        }
    }
}