using Microsoft.AspNetCore.Mvc;
using test1.InterFaces;

namespace test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _iUsers;

        public UsersController(IUsers iUsers)
        {
            _iUsers = iUsers;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var response = await _iUsers.GetAllUsersAsync(ModelState);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response);
        }
    }
}