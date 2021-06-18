using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using API.DTO;
using System;
using Microsoft.AspNetCore.Identity;
using RestAPI_Detailed.DTO;

namespace API.Controllers{
    public class ActivitiesController :BaseApiController{
        private readonly DataContext context;

        public ActivitiesController(DataContext context)
        {
            this.context = context;
        
        }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<Activity>>> GetAllActivities(){
           
           return await context.Activities.ToListAsync();
       }

       [HttpGet("{id}")]
       public async Task<ActionResult<Activity>> GetAcitivity(Guid id){
           return await context.Activities.FindAsync(id);
       } 

       [HttpPost]
       public async Task<ActionResult<String>> CreateActivity(Activity activity){
           context.Activities.Add(activity);
           await context.SaveChangesAsync();
           return Ok("Activity added.");
       }

       [HttpPut("{id}")] 
       public async Task<ActionResult<String>> UpdateActivity(Guid id, Activity activity){
            var act = await context.Activities.FindAsync(id);
            if(act == null){
                return BadRequest("No such activity exists!!");
            }
            
            act.Category = activity.Category ?? act.Category;
            act.City = activity.City ?? act.City;
            act.Date = activity.Date ?? act.Date;
            act.Description = activity.Description??act.Description;
            act.Title = activity.Title ?? act.Title;
            act.Venue = activity.Venue ?? act.Venue;
            await context.SaveChangesAsync();
            return Ok("Activity Updated");
       }

       [HttpDelete("{id}")]
        public async Task<ActionResult<String>> DeleteActivity(Guid id){
            var act = await context.Activities.FindAsync(id);
            if(act == null){
                return BadRequest("No such activity exists!!");
            }
            context.Activities.Remove(act);
            await context.SaveChangesAsync();
            return Ok("Activity Deleted.");

        }
    }
}