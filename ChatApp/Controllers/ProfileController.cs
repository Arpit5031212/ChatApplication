using ChatApp.Business.ServiceInterfaces;
using ChatApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProfileController( IProfileService profileService, IWebHostEnvironment hostingEnvironment)
        {
            _profileService = profileService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(int id, RegisterModel updatedUser)
        {
            var user = await _profileService.UpdateProfile(id, updatedUser);
            if (updatedUser == null)
            {
                return BadRequest("provide details correctly.");
            }
            await _profileService.UpdateProfile(id, updatedUser);
            return Ok(user);
        }

        [HttpPut("update-image")]

        public async Task<IActionResult> UploadProfileImage(int id, IFormFile imageFile)
        {
            var imageData = await _profileService.UpdateProfileImage(id, imageFile);

            if (imageData == null)
            {
                return BadRequest("Image cant be uploded. Please try again");
            }

            return Ok(imageData);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var user = await _profileService.GetUserProfile(id);
            if(user != null)
            {
                return Ok(user);
            }

            return BadRequest("User Not Found.");
        }

        [HttpGet("{fileName}")]
        public IActionResult GetAsset(string fileName)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", "users", $"{fileName}");
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, "image/*");
        }

    }
}
