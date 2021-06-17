using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API;
using API.DTO;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Activities.Any()) return;
            
            var activities = new List<Activity>
            {
                new Activity
                {
                    Title = "Past Activity 10",
                    Date = DateTime.Now.AddMonths(-3),
                    Description = "Activity 3 months ago",
                    Category = "drinks & drugs",
                    City = "London",
                    Venue = "Pub",
                },
                new Activity
                {
                    Title = "Past Activity 20",
                    Date = DateTime.Now.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    Category = "Party",
                    City = "Paris",
                    Venue = "Louvre",
                }
            };

            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}