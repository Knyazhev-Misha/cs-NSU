using Microsoft.EntityFrameworkCore;
 
namespace Task4
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Experiment> Experiments { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=1111;Database=cs_bd;Username=postgres;Password=1111");
        }
    }
}