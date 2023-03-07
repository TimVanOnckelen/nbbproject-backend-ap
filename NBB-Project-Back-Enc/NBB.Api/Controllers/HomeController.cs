using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NBB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {

        [HttpGet]
        [Route("/")]
        public IActionResult GetAll()
        {
            return Ok();
        }


    }
}
