using Logic.Service.Services.Interface;
using Logic.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_BackEnd.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	
	public class AdminController : Controller
	{
		private readonly IAccountService _accountService;

		public AdminController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("Register")]
		//[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Index([FromBody] SignUpViewModel Model)
		{

			return Ok(await _accountService.SignUp(Model));
		}
	}
}
