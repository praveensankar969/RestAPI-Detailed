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
    public class SeedController :ControllerBase{
        private readonly DataContext context;

        public SeedController(DataContext context)
        {
            this.context = context;
        }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<Activity>>> SeedData(){
           await Seed.SeedData(context);
           return await context.Activities.ToListAsync();
       } 
    }
}