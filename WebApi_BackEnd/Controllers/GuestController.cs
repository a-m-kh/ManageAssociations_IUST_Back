using DataBase.Configuration.Domain;
using Logic.Service.Responses;
using Logic.Service.Services;
using Logic.Service.Services.Interface;
using Logic.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_BackEnd.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class GuestController : Controller
	{
		private readonly IGuestService _guestService;
		private readonly UserManager<User> _userManager;
		private readonly IWebHostEnvironment _environment;

		public GuestController(
			IGuestService GuestService,
			UserManager<User> userManager,
			IWebHostEnvironment environment
			)
		{
			_guestService = GuestService;
			_userManager = userManager;
			_environment = environment;
		}


		[HttpGet("Get/{Id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<GetGuestResponse>))]
		[ProducesDefaultResponseType]
		public IActionResult Get(int Id)
		{
			return(Ok(_guestService.GetGuest(Id)));
		}

		[HttpGet("GetAll_Admin/{EventId}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<GetGuestResponse>>))]
		[ProducesDefaultResponseType]
		public IActionResult GetAll(int EventId)
		{
			return (Ok(_guestService.GetAllGuest(EventId)));
		}

		[HttpGet("GetAll_User/{EventId}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<GetGuestResponse>>))]
		[ProducesDefaultResponseType]
		public IActionResult GetAll_User(int EventId)
		{
			return (Ok(_guestService.GetAllGuest_WithPublic(EventId)));
		}

		[HttpPost("Create")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesDefaultResponseType]
		[Authorize]
		public async Task<IActionResult> Create([FromForm] CreateGuestViewModel Vm)
		{
			var response = new GeneralResponse<List<GetGuestResponse>>();
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
			//var userId = _userManager.GetUserId(User);
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}


			return Ok( _guestService.Create(Vm, user, _environment.WebRootPath));
		}

		[HttpPost("Update")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesDefaultResponseType]
		[Authorize]
		public async Task<IActionResult> Update([FromForm] UpdateGuestViewModel Vm)
		{
			var response = new GeneralResponse<List<GetGuestResponse>>();
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
			//var userId = _userManager.GetUserId(User);
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}


			return Ok(_guestService.Update(Vm, user, _environment.WebRootPath));
		}
		
		
		[HttpDelete("Delete/{Id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesDefaultResponseType]
		[Authorize]
		public async Task<IActionResult> Delete(int Id)
		{
			var response = new GeneralResponse<bool>();
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
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}


			return Ok(_guestService.Delete(Id, user, _environment.WebRootPath));
		}

	}
}
