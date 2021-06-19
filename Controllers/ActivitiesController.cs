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
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly DataContext context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public ActivitiesController(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            this._mapper = mapper;
            this._userAccessor = userAccessor;
            this.context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetAllActivities()
        {
            
            var activites = await context.Activities.ProjectTo<ActivityDTO>(_mapper.ConfigurationProvider).ToListAsync();
            if (activites == null)
            {
                return BadRequest("No activites no return!!");
            }
            return activites;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDTO>> GetAcitivity(Guid id)
        {
            var activites = await context.Activities.ProjectTo<ActivityDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);

            if (activites == null)
            {
                return BadRequest("No such activity!!");
            }
            return activites;
        }


        [HttpPost]
        public async Task<ActionResult<String>> CreateActivity(Activity activity)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());

            var attendee = new ActivityAttendee
            {
                AppUser = user,
                Activity = activity,
                IsHost = true
            };
            activity.Attendees.Add(attendee);
            context.Activities.Add(activity);
            await context.SaveChangesAsync();
            return Ok("Activity added.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> DeleteActivity(Guid id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
            if (user == null)
            {
                return Unauthorized();
            }

            var act = await context.Activities.Include(x => x.Attendees).ThenInclude(x => x.AppUser).FirstOrDefaultAsync(x => x.Id == id);
            if (act == null)
            {
                return BadRequest("No such activity exists!!");
            }
            var hostName = act.Attendees.SingleOrDefault(x => x.IsHost == true).AppUser.UserName;

            if (hostName == null)
            {
                return BadRequest("No such user!!, some error occured.");
            }

            if (hostName != user.UserName)
            {
                return Unauthorized();
            }

            context.Activities.Remove(act);
            await context.SaveChangesAsync();
            return Ok("Activity Deleted.");

        }

        [HttpPost]
        [Route("{id}/attend")]
        public async Task<ActionResult<String>> AttendActivity(Guid id)
        {
            string message = "";
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
            if (user == null)
            {
                return Unauthorized();
            }
            var activity = await context.Activities.Include(x => x.Attendees).ThenInclude(x => x.AppUser).FirstOrDefaultAsync(x => x.Id == id);
            if (activity == null)
            {
                return BadRequest("No such Activity!!");
            }
            var hostName = activity.Attendees.FirstOrDefault(x => x.IsHost == true)?.AppUser?.UserName;
            if (hostName == user.UserName)
            {
                return Ok("Host is already an attendee!!");
            }
            var activityAttendee = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);
            if (activityAttendee != null && hostName == user.UserName)
            {
                activity.IsCancelled = !activity.IsCancelled;
                message = "Activity Cancelled";
            }
            if (activityAttendee != null && hostName != user.UserName)
            {
                activity.Attendees.Remove(activityAttendee);
                message = "Not attending activity anymore!";
            }
            if (activityAttendee == null)
            {
                activityAttendee = new ActivityAttendee
                {
                    IsHost = false,
                    AppUser = user,
                    Activity = activity
                };
                activity.Attendees.Add(activityAttendee);
                message = "Attending Activity!!";
            }

            await context.SaveChangesAsync();

            return Ok(message);

        }

        [HttpPut]
        [Route("{id}/update")]
        public async Task<ActionResult<string>> UpdateActivity(Guid id, [FromBody] Activity _activity)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
            if (user == null)
            {
                return Unauthorized();
            }

            // var userId = _userAccessor.GetUserId();
            // var guid = _userAccessor.GetActivityId();
            var activity = await context.Activities.Include(x => x.Attendees).ThenInclude(x => x.AppUser).FirstOrDefaultAsync(x => x.Id == id);

            if (activity == null)
            {
                return BadRequest("No such activity!!");
            }

            var hostName = activity.Attendees.SingleOrDefault(x => x.IsHost == true).AppUser.UserName;

            if (hostName == null)
            {
                return BadRequest("No such user!!, some error occured.");
            }

            if (hostName != user.UserName)
            {
                return Unauthorized();
            }


            activity.Category = _activity.Category ?? activity.Category;
            activity.Venue = _activity.Venue ?? activity.Venue;
            activity.Description = _activity.Description ?? activity.Description;
            activity.Title = _activity.Title ?? activity.Title;
            activity.City = _activity.City ?? activity.City;
            activity.Date = _activity.Date ?? activity.Date;

            await context.SaveChangesAsync();

            return Ok("Activity Updated Sucessfully");

        }

    }
}