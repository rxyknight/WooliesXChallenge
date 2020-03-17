using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WooliesXChallenge.Models;
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

        // GET api/v2/trolley/trolleyTotal
        // This answer the option question for exercise3
        [HttpPost("api/v2/trolley/trolleyTotal")]
        public IActionResult Post2([FromBody]Trolley trolley)
        {
            return Ok(_trolleyService.CalculateTrolleyTotalLocal(trolley));
        }
    }
}
