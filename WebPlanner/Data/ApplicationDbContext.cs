using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebPlanner.Models;

namespace WebPlanner.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<EventUsersSigned>()
           .HasKey(t => new { t.EventId, t.UserId });

            builder.Entity<EventUsersSigned>()
                .HasOne(sc => sc.Event)
                .WithMany(s => s.EventUsersSigned)
                .HasForeignKey(sc => sc.EventId);

            builder.Entity<EventUsersSigned>()
                .HasOne(sc => sc.User)
                .WithMany(c => c.EventUsersSigned)
                .HasForeignKey(sc => sc.UserId);




        }
    }
}
