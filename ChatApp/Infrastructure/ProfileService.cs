using ChatApp.Business.ServiceInterfaces;
using ChatApp.Context;
using ChatApp.Context.EntityClasses;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure
{
    public class ProfileService : IProfileService
    {

        private readonly ChatAppContext context;
        private readonly IWebHostEnvironment environmement;

        public ProfileService(ChatAppContext context, IWebHostEnvironment _environment) 
        {
            environmement = _environment;
            this.context = context;
        }

        public async Task<RegisterModel> GetUserProfile(int id)
        {
            UserProfile user = null;

            if(id != 0)
            {
                user = await context.UserProfiles.FirstOrDefaultAsync(u => u.Id == id);
            }

            RegisterModel userProfile = null;

            if(user == null)
            {
                return userProfile;
            }

            userProfile = new RegisterModel
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                ImageName = user.ImageName,
                PhoneNumber = user.PhoneNumber,
            };
            return userProfile;
        }

        // Register User
        public async Task<UserProfile> RegisterUser(RegisterModel regModel)
        {
            UserProfile? newUser = null;
            if (!CheckEmailOrUserNameExists(regModel.Username, regModel.Email))
            {
                string hashedPass = BCrypt.Net.BCrypt.HashPassword(regModel.Password);

                newUser = new UserProfile
                {
                    FirstName = regModel.FirstName,
                    LastName = regModel.LastName,
                    Password = hashedPass,
                    Username = regModel.Username,
                    Email = regModel.Email,
                    PhoneNumber = regModel.PhoneNumber,
                    DateOfBirth = regModel.DateOfBirth,
                    Gender = regModel.Gender,

                    ImageUrl = "/Images/default.png",
                    ImageName = "default.png",

                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    CreatedBy = 0,
                    LastUpdatedBy = 0,
                    

                };
                await context.UserProfiles.AddAsync(newUser);
                await context.SaveChangesAsync();
            }
            return newUser;
        }

        // Update profile image
        public async Task<ProfileImageUploadModel> UpdateProfileImage(int id, IFormFile imageFile)
        {
            var user = await context.UserProfiles.Where(u => u.Id == id).FirstOrDefaultAsync();

            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(environmement.WebRootPath, @"Images/Users/");

            var extension = Path.GetExtension(imageFile.FileName);
            string[] allowedExtensions = new string[] { ".jpeg", ".png", ".jpg" };

            if(!allowedExtensions.Contains(extension))
            {
                return null;
            }

            if (user.ImageUrl != null || !(user.ImageUrl == "" || !Equals("Images/default.png")))
            {
                var oldImagePath = Path.Combine(environmement.WebRootPath + user.ImageUrl);
                if(File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            using (var fileStreams = new FileStream(Path.Combine(uploads + fileName + extension), FileMode.Create))
            {
                imageFile.CopyTo(fileStreams);
            }

            user.ImageUrl = "/Images/Users/" + fileName + extension;
            user.ImageName = fileName + extension;

            context.UserProfiles.Update(user);
            await context.SaveChangesAsync();

            ProfileImageUploadModel imageModel = new ProfileImageUploadModel()
            {
                ProfileImage = imageFile,
            };

            return imageModel;
        }



        public async Task<UserProfile> UpdateProfile(int id, RegisterModel updatedUser)
        {
            var user = await context.UserProfiles.FindAsync(id);
            if(user !=  null && updatedUser != null)
            {
                user.Username = updatedUser.Username;
                user.Email = updatedUser.Email;
                user.PhoneNumber = updatedUser.PhoneNumber;
                user.DateOfBirth = updatedUser.DateOfBirth;
                user.Gender = updatedUser.Gender;
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Password = updatedUser.Password;

                context.UserProfiles.Update(user);
                await context.SaveChangesAsync();
                return user;
            }
            return null;
        }

        // verify password
        public async Task<UserProfile> CheckPassword(LoginModel loginModel)
        {
            return await context.UserProfiles.FirstOrDefaultAsync(u => u.Password == BCrypt.Net.BCrypt.HashPassword(loginModel.Password) && u.Email.ToLower().Trim() == loginModel.Email.ToLower().Trim() || u.Username.ToLower().Trim() == loginModel.Username.ToLower().Trim());
        }

        // check if user exists or not
        private bool CheckEmailOrUserNameExists(string userName, string email)
        {
            return context.UserProfiles.Any(u => u.Username.ToLower().Trim() == userName.ToLower().Trim() || u.Email.ToLower().Trim() == email.ToLower().Trim());
        }
    }
}
