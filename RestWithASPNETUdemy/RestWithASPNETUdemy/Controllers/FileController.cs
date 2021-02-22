using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Controllers
{
	[ApiVersion("1")]
	[ApiController]
	[Authorize("Bearer")]
	[Route("api/[controller]/v{version:apiVersion}")]
	public class FileController : ControllerBase
	{
		private readonly IFileBusiness _fileBusiness;

		public FileController(IFileBusiness fileBusiness)
		{
			_fileBusiness = fileBusiness;
		}

		[HttpPost("uploadFile")]
		[ProducesResponseType((200), Type = typeof(FileDetailVO))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
		{
			FileDetailVO detail = await _fileBusiness.SaveFileToDisk(file);

			return Ok(detail);
		}


		[HttpGet("downloadFile/{fileName}")]
		[ProducesResponseType((200), Type = typeof(byte[]))]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> GetFileAsync(string fileName)
		{
			byte[] buffer =  _fileBusiness.GetFile(fileName);
			if (buffer != null)
			{
				HttpContext.Response.ContentType = 
					$"application/{Path.GetExtension(fileName).Replace(".","")}";
				HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
				var response = HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
				await response;
			}

			return Ok();
		}

		[HttpPost("uploadManyFiles")]
		[ProducesResponseType((200), Type = typeof(FileDetailVO))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> UploadManyFile([FromForm] IList<IFormFile> files)
		{
			IList<FileDetailVO> details = await _fileBusiness.SaveFilesToDisk(files);

			return Ok(details);
		}
	}
}
