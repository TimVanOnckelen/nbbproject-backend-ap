using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBB.Api.Models;
using NBB.Api.Repository;

namespace NBB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OndernemingController : Controller
    {
        private IRepository _repository;

        public OndernemingController(IRepository repo)
        {
            this._repository = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var ondernemingen = _repository.GetAll();
            return Ok(ondernemingen);
        }


        [HttpGet("{ondernemingsnumer}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string ondernemingsnumer)
        {
            var onderneming = _repository.Get(ondernemingsnumer);
            return Ok(onderneming);
        }


    }
}
