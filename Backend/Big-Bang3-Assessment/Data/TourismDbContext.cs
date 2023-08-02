using System.Collections.Generic;
using Big_Bang3_Assessment.Model;
using Microsoft.EntityFrameworkCore;

namespace Big_Bang3_Assessment.Data
{
    public class TourismDbContext : DbContext
    {
        public TourismDbContext(DbContextOptions<TourismDbContext> options) : base(options)
        {
        }

        public DbSet<AdminRegister> adminRegisters { get; set; }

        public DbSet<AgentRegister> agentRegisters { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<Agency> agencies { get; set; }

        public DbSet<AccommodationDetail> accommodations { get; set;}

        public DbSet<Big_Bang3_Assessment.Model.Booking> Booking { get; set; }

        public DbSet<FeedBack> feedBacks { get; set; }

        public DbSet<Big_Bang3_Assessment.Model.AdminPost> AdminPost { get; set; }

    }
}
