using Microsoft.EntityFrameworkCore;
using TheatreAPIsAssignment.Contracts.Models;
namespace TheatreAPIsAssignment.Contracts.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Show> Shows { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>(entity => {
                entity.HasKey(a => a.Id);

                entity.HasMany(a=>a.Shows).WithOne(p=>p.Movie).OnDelete(DeleteBehavior.Cascade);

            });


            modelBuilder.Entity<Show>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasOne(a => a.Movie).WithMany(p=>p.Shows).HasForeignKey(a=>a.MovieId).OnDelete(DeleteBehavior.Cascade);
            });



        }
    }
}
