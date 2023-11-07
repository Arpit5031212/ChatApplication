using ChatApp.Context.EntityClasses;
using ChatApp.Models;

namespace ChatApp.Business.ServiceInterfaces
{
    public interface IProfileService
    {
        Task<UserProfile> CheckPassword(LoginModel loginModel);
        Task<UserProfile> RegisterUser(RegisterModel regModel);
        Task<UserProfile> UpdateProfile(int id, RegisterModel registerModel);
        Task<ProfileImageUploadModel> UpdateProfileImage(int id,  IFormFile imgData);
        Task<RegisterModel> GetUserProfile(int id);
    }
}