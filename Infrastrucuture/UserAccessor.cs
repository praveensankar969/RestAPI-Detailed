using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RestAPI_Detailed.Interfaces;

namespace RestAPI_Detailed.Infrastructure
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        public string GetUserId(){
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public Guid GetActivityId(){
            var guid =  Guid.Parse(_httpContextAccessor.HttpContext?.Request?.RouteValues.SingleOrDefault(x=> x.Key == "id").Value.ToString());
            return guid;
        }


    }
}