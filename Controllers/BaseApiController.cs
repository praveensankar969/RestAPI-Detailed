using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Persistence;
using API.DTO;

namespace API.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController :ControllerBase{

    }
}