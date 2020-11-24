using System.Security.Claims;
using LMS.Models.ViewModels;

namespace LMS.Services
{
    public interface INextActivityService
    {
        TeacherActivityViewModel GetActivity(ClaimsPrincipal user);
    }
}