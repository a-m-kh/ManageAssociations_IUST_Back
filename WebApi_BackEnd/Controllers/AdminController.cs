using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_BackEnd.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	
	public class AdminController : Controller
	{

		[HttpPost("edit_profile")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public IActionResult Index()
		{
			return Ok();
		}
	}
}
