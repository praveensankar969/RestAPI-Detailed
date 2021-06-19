using System;
using Microsoft.EntityFrameworkCore;
using API.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RestAPI_Detailed.DTO;

namespace API
{
    public class DataContext: IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options ): base(options){
        }

        public DataContext()
        {
            
        }

        public DbSet<Activity> Activities {get; set;}

        public DbSet<ActivityAttendee> AcitivityAttendees {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee>(x=> x.HasKey(aa=> new {aa.ActivityId, aa.AppUserId}));

            builder.Entity<ActivityAttendee>().HasOne(x=>x.AppUser).WithMany(x=> x.Activities).HasForeignKey(x=> x.AppUserId);

            builder.Entity<ActivityAttendee>().HasOne(x=> x.Activity).WithMany(x=> x.Attendees).HasForeignKey(x=> x.ActivityId);
        }
    }
}
