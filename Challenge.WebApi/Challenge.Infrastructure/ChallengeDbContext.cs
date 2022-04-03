using Challenge.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure
{
    public class ChallengeDbContext : DbContext
    {
        public ChallengeDbContext(DbContextOptions<ChallengeDbContext> options) : base(options) { }

        public ChallengeDbContext()
        {

        }

        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessage { get; set; }
        public DbSet<ChallengeUser> AspNetUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChatRoom>().HasData(
               new ChatRoom
               {
                   Id = 1,
                   Name = "IRL"
               },
               new ChatRoom
               {
                   Id = 2,
                   Name = "Sports"               
               },
               new ChatRoom
               {
                   Id = 3,
                   Name = "Politics"
               }
           );
        }

    }

}