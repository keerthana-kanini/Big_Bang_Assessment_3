using Big_Bang_Assessment_3.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Big_Bang_Assessment_3.DBContext
{
    public class TourismContext : DbContext
    {
        public DbSet<Travellers> travellers { get; set; }
        public DbSet<TravelAgents> Agents { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet <Agency> Agencies { get; set; }

        public TourismContext(DbContextOptions<TourismContext> options) : base(options)
        {

        }
    }
}
