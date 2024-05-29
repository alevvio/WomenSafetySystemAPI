using Microsoft.EntityFrameworkCore;
using WomenSafetySystemApi.Models.Domain;

namespace WomenSafetySystemApi.Data
{
    public class WSSDbContext: DbContext
    {
        public WSSDbContext(DbContextOptions<WSSDbContext> options) : base(options)
        {

        }

        //public DbSet<UserInfo> Users { get; set; }
        public DbSet<ContactInfo> Contacts { get; set; }
        public DbSet<IncidentInfo> Incidents { get; set; }
    }
}