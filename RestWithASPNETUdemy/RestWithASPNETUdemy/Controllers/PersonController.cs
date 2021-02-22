using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;
using Serilog;

namespace RestWithASPNETUdemy.Controllers
{
	[ApiVersion("1")]
	[ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
       
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, 
            IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

      
        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get([FromQuery] string name, string sortDirection, int pageSize, int page)
		{
            return Ok(_personBusiness.FindWithPagedSearch(name, sortDirection, pageSize, page));
		}

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
		{
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();

            return Ok(person);
		}

        [HttpGet("findByName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get([FromQuery]string firstName, [FromQuery]string secondName)
        {
            var person = _personBusiness.FindByName(firstName, secondName);
            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] PersonVO person)
		{
            if (person == null) return BadRequest();

            return Ok(_personBusiness.Create(person));
		}

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] PersonVO person)
		{
            if (person == null) BadRequest();

            return Ok(_personBusiness.Update(person));
		}

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Patch(long id)
        {
            return Ok(_personBusiness.Disable(id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
		{
            _personBusiness.Delete(id);

            return NoContent();
		}
	}
}
