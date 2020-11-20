using System.Security.Claims;
using LMS.Models;

namespace LMS.Services
{
    public interface INextActivityService
    {
        Aktivitet GetActivity(ClaimsPrincipal user);
    }
}