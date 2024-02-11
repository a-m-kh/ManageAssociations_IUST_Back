using DataBase.Configuration.Domain;
using Logic.Service.Responses;
using Logic.Service.Services.Interface;
using Logic.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_BackEnd.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly UserManager<User> _userManager;
		public AccountController(IAccountService accountService,UserManager<User> userManager)
		{
			_accountService = accountService;
			_userManager = userManager;
		}


		[HttpPost("Login")]
		[ProducesResponseType(StatusCodes.Status200OK, Type =typeof(GeneralResponse<LoginResponse>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Index([FromBody] LoginViewModel Model)
		{

			var response = new GeneralResponse<LoginResponse>();
			if (!ModelState.IsValid)
			{
				var errors = string.Join(" | ", ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage));
				response.IsSuccess = false;
				response.Message = errors;
				return Ok(response);
			}

			return Ok(await _accountService.Login(Model));
		}


		[HttpPost("edit_profile")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<EditProfileResponse>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> edit_profile([FromBody] EditProfileViewModel Model)
		{

			var response = new GeneralResponse<EditProfileResponse>();
			if (!ModelState.IsValid)
			{
				var errors = string.Join(" | ", ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage));
				response.IsSuccess = false;
				response.Message = errors;
				return Ok(response);
			}
			System.Security.Claims.ClaimsPrincipal currentUser = this.User;
			var userId = _userManager.GetUserId(User);

			if(userId == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}
			return Ok(await _accountService.EditProfile(Model , userId));
		}
	}
}
