using Microsoft.EntityFrameworkCore;
using Startup.Domain.Entity;

namespace StartupProject.Data
{
    public class AppDbContext: DbContext
    {
           
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Alternative> Alternative { get; set; }
        public DbSet<Response> Response { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Quiz>()
                .HasMany(q => q._Perguntas)
                .WithOne(qn => qn.quiz)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Question>()
                .HasMany(qn => qn.alternatives)
                .WithOne(a => a.question)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Alternative>()
                .HasMany(a => a.responses)
                .WithOne(r => r.alternative)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
