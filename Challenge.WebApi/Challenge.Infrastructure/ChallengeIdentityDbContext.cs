using Challenge.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure
{
    public class ChallengeIdentityDbContext : IdentityDbContext<ChallengeUser>
    {
        public ChallengeIdentityDbContext(DbContextOptions<ChallengeIdentityDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding a  'Administrator' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c7a3320-0d7f-4b8a-88d7-93d855462f90", Name = "User", NormalizedName = "User".ToUpper() });


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ChallengeUser>();


            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<ChallengeUser>().HasData(
                new ChallengeUser
                {
                    Id = "21397611-90c1-4359-94d0-0800eb2a4f5b", // primary key
                    UserName = "goku",
                    NormalizedUserName = "GOKU",
                    PasswordHash = hasher.HashPassword(null, "Kamehameha")
                }
            );

            modelBuilder.Entity<ChallengeUser>().HasData(
                new ChallengeUser
                {
                    Id = "25caed64-ae6c-4069-bb72-554ad038498e", // primary key
                    UserName = "krilin",
                    NormalizedUserName = "krilin".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "AlwaysDie")
                }
            );

            modelBuilder.Entity<ChallengeUser>().HasData(
                new ChallengeUser
                {
                    Id = "c09e1e40-dc1f-45b7-8b52-b65145954d94", // primary key
                    UserName = "vegeta",
                    NormalizedUserName = "vegeta".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "PrinceSSJ")
                }
            );

            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c7a3320-0d7f-4b8a-88d7-93d855462f90",
                    UserId = "21397611-90c1-4359-94d0-0800eb2a4f5b"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c7a3320-0d7f-4b8a-88d7-93d855462f90",
                    UserId = "25caed64-ae6c-4069-bb72-554ad038498e"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c7a3320-0d7f-4b8a-88d7-93d855462f90",
                    UserId = "c09e1e40-dc1f-45b7-8b52-b65145954d94"
                }
            );

        }
    }
}
