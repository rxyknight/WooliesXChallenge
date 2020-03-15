using Microsoft.AspNetCore.Mvc;
using WooliesXChallenge.Models;

namespace WooliesXChallenge.Controllers
{
    //
    // Summary:
    //      UserController, this controller is for exercise1
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET api/user
        [HttpGet]
        public IActionResult Get()
        {
            var user = new User
            {
                Name = "Roy Ren",
                Token = "76c7f5a2-0f75-48fb-811e-1c2e9352a459"
            };
            return Ok(user);
        }
    }
}
