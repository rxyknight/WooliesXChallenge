using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WooliesXChallenge.Services.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WooliesXChallenge.Controllers
{
    //
    // Summary:
    //      TrolleyController, this controller is for exercise3 
    public class TrolleyController : ControllerBase
    {
        private readonly ITrolleyService _trolleyService;
        public TrolleyController(ITrolleyService trolleyService)
        {
            _trolleyService = trolleyService;
        }
        // GET api/trolley/trolleyTotal
        [HttpPost("api/trolley/trolleyTotal")]
        public IActionResult Post([FromBody]JToken request)
        {
            return Ok(_trolleyService.CalculateTrolleyTotal(request));
        }
    }
}
