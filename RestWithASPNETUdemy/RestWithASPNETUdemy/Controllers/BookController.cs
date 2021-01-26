using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.VO;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Controllers
{
	[ApiVersion("1")]
	[ApiController]
	[Route("api/[controller]/v{version:apiVersion}")]
	public class BookController : ControllerBase
	{
		private readonly IBookBusiness _bookBusiness;

		public BookController(IBookBusiness bookBusiness)
		{
			_bookBusiness = bookBusiness;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<BookVO>))]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult Get()
		{
			var result = _bookBusiness.FindAll();
			if (result == null) return BadRequest();

			return Ok(result);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(BookVO))]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult Get(long id)
		{
			var result = _bookBusiness.FindById(id);
			if (result == null) return NotFound();

			return Ok(result);
		}		

		[HttpPost]
		[ProducesResponseType(200, Type = typeof(BookVO))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult Post([FromBody] BookVO book)
		{
			return Ok(_bookBusiness.Create(book));
		}

		[HttpPut]
		[ProducesResponseType(200, Type = typeof(BookVO))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult Put([FromBody] BookVO book)
		{
			return Ok(_bookBusiness.Update(book));
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult Delete(long id)
		{
			var result = _bookBusiness.FindById(id);
			if (result == null) return NotFound();

			_bookBusiness.Delete(id);
			return NoContent();
		}
	}
}
