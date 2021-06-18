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
    }
}
