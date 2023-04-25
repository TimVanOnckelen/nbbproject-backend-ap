using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NBB.Api.Models;
using NBB.Api.Repository;

namespace NBB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnterpriseController : Controller
    {
        private IRepository<Enterprise> _repository;

        public EnterpriseController(IRepository<Enterprise> repo)
        {
            this._repository = repo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Enterprise>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var ondernemingen = await _repository.GetAll();
            return Ok(ondernemingen);
        }


        [HttpGet("{ondernemingsnummer}")]
        [ProducesResponseType(typeof(Enterprise),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string ondernemingsnummer)
        {
            var onderneming = await _repository.Get(ondernemingsnummer);

            if (onderneming == null)
            {
                return NotFound();
            }

            return Ok(onderneming);
        }

        [HttpGet("{ondernemingsnummer}/{financialYear}")]
        [ProducesResponseType(typeof(FinancialData),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByYear(string ondernemingsnummer, int financialYear)
        {
            var onderneming = await _repository.Get(ondernemingsnummer);
            if(onderneming.FinancialDataArray == null)
            {
                return NotFound();
            }

            var financialYearData = onderneming.FinancialDataArray.Find(data => data.Year == financialYear);


            if(financialYearData == null)
            {
                return NotFound();
            }

            return Ok(financialYearData);
        }
    }
}
