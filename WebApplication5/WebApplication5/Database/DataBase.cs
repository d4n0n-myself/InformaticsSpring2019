using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Database
{
    public class DataBase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=ec2-54-228-212-134.eu-west-1.compute.amazonaws.com;Database=dcu8uj5b161r1j;Username=bgyujqimyrrdek;Port=5432;Password=6aee2a2c75e5334a4db112e6153b5ead71ac3ae0bd143078a49ce2ad534184b5;SslMode=Require;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasIndex(n => n.Header);
        }

        public DbSet<Note> Notes { get; set; }
    }
}