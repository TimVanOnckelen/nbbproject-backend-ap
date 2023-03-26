using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBB.Api.Models;
using NBB.Api.Repository;

namespace NBB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnterpriseController : Controller
    {
        private IRepository _repository;

        public EnterpriseController(IRepository repo)
        {
            this._repository = repo;
        }
        
        /// <summary>
        /// Default call. Geeft alle ondernemingen terug die voorkomen in _repository
        /// </summary>
        /// <returns>
        /// IEnumarable<Enterprise>
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Enterprise>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var ondernemingen = _repository.GetAll();
            return Ok(ondernemingen);
        }

        /// <summary>
        /// Vraag de gegevens van een specifieke onderneming op.
        /// </summary>
        /// <param name="ondernemingsnummer"></param>
        /// <returns>Enterprise</returns>
        [HttpGet("{ondernemingsnummer}")]
        [ProducesResponseType(typeof(Enterprise),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string ondernemingsnummer)
        {
            var onderneming = _repository.Get(ondernemingsnummer);

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
        public IActionResult GetByYear(string ondernemingsnummer, int financialYear)
        {
            var onderneming = _repository.Get(ondernemingsnummer);
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
