using Challenge.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure
{
    public class ChallengeDbContext : IdentityDbContext<ChallengeUser>
    {
        public ChallengeDbContext(DbContextOptions<ChallengeDbContext> options) : base(options) { }

        public ChallengeDbContext()
        {

        }

    }

}