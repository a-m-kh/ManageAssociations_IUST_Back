using Logic.Service.Responses;
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
		[Authorize(Roles ="SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Index([FromBody] SignUpViewModel Model)
		{
			var response = new GeneralResponse<string>();
			if (!ModelState.IsValid)
			{
				var errors = string.Join(" | ", ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage));
				response.IsSuccess = false;
				response.Message = errors;
				return Ok(response);
			}
			return Ok(await _accountService.SignUp(Model));
		}

		[HttpPost("Assign")]
		[Authorize(Roles = "SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Assign([FromBody] AssignViewModel Model)
		{
			var response = new GeneralResponse<string>();
			if (!ModelState.IsValid)
			{
				var errors = string.Join(" | ", ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage));
				response.IsSuccess = false;
				response.Message = errors;
				return Ok(response);
			}
			return Ok(await _accountService.Assign(Model.UserId , Model.AssociationId));
		}
	}
}
