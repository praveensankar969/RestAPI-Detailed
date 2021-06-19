using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using API.DTO;
using System;
using Microsoft.AspNetCore.Identity;
using RestAPI_Detailed.DTO;
using Microsoft.AspNetCore.Authorization;
using RestAPI_Detailed.Interfaces;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly DataContext context;
        private readonly IUserAccessor _userAccessor;

        public ActivitiesController(DataContext context, IUserAccessor userAccessor)
        {
            this._userAccessor = userAccessor;
            this.context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetAllActivities()
        {

            return await context.Activities.Include(x=> x.Attendees).ThenInclude(x=> x.AppUser).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetAcitivity(Guid id)
        {
            return await context.Activities.FindAsync(id);
        }


        [HttpPost]
        public async Task<ActionResult<String>> CreateActivity(Activity activity)
        {
            var user = await context.Users.FirstOrDefaultAsync(x=> x.UserName == _userAccessor.GetUserName());

            var attendee = new ActivityAttendee{
                AppUser = user,
                Activity = activity,
                IsHost =true
            };
            activity.Attendees.Add(attendee);
            context.Activities.Add(activity);
            await context.SaveChangesAsync();
            return Ok("Activity added.");
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<String>> UpdateActivity(Guid id, Activity activity)
        {
            var act = await context.Activities.FindAsync(id);
            if (act == null)
            {
                return BadRequest("No such activity exists!!");
            }

            act.Category = activity.Category ?? act.Category;
            act.City = activity.City ?? act.City;
            act.Date = activity.Date ?? act.Date;
            act.Description = activity.Description ?? act.Description;
            act.Title = activity.Title ?? act.Title;
            act.Venue = activity.Venue ?? act.Venue;
            await context.SaveChangesAsync();
            return Ok("Activity Updated");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> DeleteActivity(Guid id)
        {
            var act = await context.Activities.FindAsync(id);
            if (act == null)
            {
                return BadRequest("No such activity exists!!");
            }
            context.Activities.Remove(act);
            await context.SaveChangesAsync();
            return Ok("Activity Deleted.");

        }
    }
}