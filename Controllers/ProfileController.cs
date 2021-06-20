using System.Threading.Tasks;
using API;
using API.Controllers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI_Detailed.DTO;
using RestAPI_Detailed.Interfaces;

namespace RestAPI_Detailed.Controllers
{
    public class ProfileController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;
        public ProfileController(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            this._mapper = mapper;
            this._userAccessor = userAccessor;
            this._context = context;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<RestAPI_Detailed.DTO.Profile>> GetProfile(string userName)
        {

            var user = await _context.Users.ProjectTo<RestAPI_Detailed.DTO.Profile>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(x=> x.UserName == userName);

            return user;

        }
    }
}