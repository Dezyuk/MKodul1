using Microsoft.EntityFrameworkCore;
using MKodul1.Entity;

namespace MKodul1.Context
{
    public class QuizDBContext : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public QuizDBContext(DbContextOptions<QuizDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Answer)
                .WithMany() 
                .HasForeignKey(q => q.AnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Categories)
                .WithMany(c => c.Questions)
                .UsingEntity(j => j.ToTable("CategoryQuestion"));
        }

    }

}
