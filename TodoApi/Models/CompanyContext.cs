using Microsoft.EntityFrameworkCore;

namespace CompanyApi.Models
{
    public class CompanyContext : DbContext

    {
         public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public CompanyContext() {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;user=ktp-admin;password=!4AdeAde;database=knowthepro");



        }

        public DbSet<CompanyItem> Companies { get; set; }
    }
}