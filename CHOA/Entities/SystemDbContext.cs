using Microsoft.EntityFrameworkCore;

namespace CHOA.Entities
{
    public class SystemDbContext : DbContext
    {
        private string connectionString;
        public DbSet<Algorithm> Algorithms { get; set; }
        public DbSet<FitnessFunction> FitnessFunctions { get; set; }
        public DbSet<TestResults> TestResults { get; set; }
        public DbSet<Tests> Tests { get; set; }
        public DbSet<Sessions> Sessions { get; set; }

        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = "Server=localhost;Database=NazwaBazy;User=TwojUzytkownik;Password=TwojeHaslo;";
            //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Algorithm>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Algorithm>()
                .Property(a => a.FileName)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<FitnessFunction>()
                .Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<FitnessFunction>()
                .Property(f => f.FileName)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<TestResults>()
                .Property(t => t.TestId)
                .IsRequired();

            modelBuilder.Entity<TestResults>()
                .Property(t => t.Parameters)
                .IsRequired();

            modelBuilder.Entity<Sessions>()
                .Property(s => s.AlgorithmIds)
                .IsRequired();

            modelBuilder.Entity<Sessions>()
                .Property(s => s.FitnessFunctionIds)
                .IsRequired();

            modelBuilder.Entity<Sessions>()
                .Property(s => s.State)
                .IsRequired();
        }
    }
}
