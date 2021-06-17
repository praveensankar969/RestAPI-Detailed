using System;
using Microsoft.EntityFrameworkCore;
using API.DTO;

namespace API
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options ): base(options){
        }

        public DataContext()
        {
            
        }

        public DbSet<Activity> Activities {get; set;}
    }
}
