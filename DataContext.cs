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

        public DbSet<Photo> Photos {get; set;}

        public DbSet<Comment> Comments {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Comment>().HasOne(a => a.Activity).WithMany(c => c.Comments).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ActivityAttendee>(x=> x.HasKey(aa=> new {aa.ActivityId, aa.AppUserId}));

            builder.Entity<ActivityAttendee>().HasOne(x=>x.AppUser).WithMany(x=> x.Activities).HasForeignKey(x=> x.AppUserId);

            builder.Entity<ActivityAttendee>().HasOne(x=> x.Activity).WithMany(x=> x.Attendees).HasForeignKey(x=> x.ActivityId);

            

        }
    }
}
