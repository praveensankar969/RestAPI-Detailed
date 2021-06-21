using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CommentController : BaseApiController
    {
        private readonly IUserAccessor _userAccessor;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CommentController(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
            this._userAccessor = userAccessor;
        }

        [HttpGet("{activityId}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComment(Guid activityId)
        {
            var activity = await _context.Activities.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == activityId);
            if (activity == null)
            {
                return BadRequest("No such activity!!");
            }
            var comments = _context.Comments.Where(x => x.Activity.Id == activityId).OrderBy(x => x.CreatedAt);

            var returnDTO = _mapper.Map<List<CommentDTO>>(comments);
            
            return returnDTO;
        }

        [HttpPost("{activityId}")]
        public async Task<ActionResult<CommentDTO>> AddComment(Guid activityId, [FromBody] MessageDTO message){
            if(message.Message == null){
                return BadRequest("Empty comment not allowed");
            }
            var user = await _context.Users.Include(x=> x.Photos).FirstOrDefaultAsync(x=> x.UserName == _userAccessor.GetUserName());
            if(user == null){
                return Unauthorized();
            }
            var activity = await _context.Activities.FirstOrDefaultAsync(x=> x.Id == activityId);
             if (activity == null)
            {
                return BadRequest("No such activity!!");
            }

            var comment = new Comment{
                Body = message.Message,
                Author = user,
                Activity = activity
            };

            activity.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var returnDTO = _mapper.Map<CommentDTO>(comment);
            
            return returnDTO;
        }
    }
}