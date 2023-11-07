using ChatApp.Business.ServiceInterfaces;
using ChatApp.Context.EntityClasses;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralService generalService;
        public GeneralController(IGeneralService _generalService)
        {
            this.generalService = _generalService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchResults(string searchStr, int currentUserId)
        {
            List<SearchResult> searchResults = await generalService.SearchUser(searchStr, currentUserId);
            if(searchResults.Count == 0)
            {
                return Ok("No users found.");
            }

            return Ok(searchResults);
        }

        [HttpGet("chat-with")]
        public async Task<IActionResult> GetUser(int id)
        {
            SearchedUser user = await generalService.GetUserById(id);
            if(user != null)
            {
                return Ok(user);
            }

            return Ok("no user found");
        }
       
    }
}
