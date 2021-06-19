using System;

namespace RestAPI_Detailed.Interfaces
{
    public interface IUserAccessor
    {
        string GetUserName();

        string GetUserId();

        Guid GetActivityId();
    }
}