using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Persistence;
using API.DTO;

namespace API.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class ActivitiesController :ControllerBase{
        private readonly DataContext context;

        public ActivitiesController(DataContext context)
        {
            this.context = context;
        }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<Activity>>> GetAllActivities(){
           return await context.Activities.ToListAsync();
       } 
    }
}