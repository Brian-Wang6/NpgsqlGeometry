using Microsoft.EntityFrameworkCore;
using NpgsqlGeometry.Model;
using EntityFramework.Exceptions.PostgreSQL;

namespace NpgsqlGeometry.DBContext
{
    public class PostgreDBContext : DbContext
    {
        public PostgreDBContext() { }

        public PostgreDBContext(DbContextOptions<PostgreDBContext> options) : base(options) { }

        public virtual DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            SetupCommonConfigModels(modelBuilder);
        }

        private void SetupCommonConfigModels(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>();
        }
    }
}
