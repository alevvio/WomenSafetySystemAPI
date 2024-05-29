using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WomenSafetySystemApi.Data
{
    public class WSSAuthDbContext: IdentityDbContext
    {
        public WSSAuthDbContext(DbContextOptions<WSSAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var userRoleId = "26f797fa-2947-42c8-8ca6-3f85d3bc067a";
            var adminRoleId = "077c6cda-d88b-44c6-8776-c828c74da928";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}