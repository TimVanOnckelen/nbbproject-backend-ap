using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NBB.Api.Models;
using NBB.Api.Services;
using NBB.Api.ViewModels;

namespace NBB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnterpriseController : Controller
    {
        private readonly IEnterpriseRepository _repository;

        public EnterpriseController(IEnterpriseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Enterprise>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var enterprises = _repository.GetAll();
            if (enterprises == null) return NotFound();

            return Ok(enterprises);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Enterprise), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string id)
        {
            var enterprise = _repository.Get(id);
            if (enterprise == null) return NotFound();

            return Ok(enterprise);
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Enterprise>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDouble([FromBody]EnterpriseArrayViewModel EntArray)
        {
            var enterprises = new List<Enterprise>();
            EntArray.EnterpriseNumbers.ForEach(e =>
            {
                var enterprise = _repository.Get(e);
                if(enterprise != null) enterprises.Add(enterprise);
            });

            if(enterprises.Count == 0) return NotFound();

            return Ok(enterprises);
        }

    }
}
