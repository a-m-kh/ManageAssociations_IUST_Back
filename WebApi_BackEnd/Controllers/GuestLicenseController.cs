using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
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
	public class GuestLicenseController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly IGuestLicenseService _guestLicenseService;

		public GuestLicenseController(UserManager<User> userManager, IGuestLicenseService guestLicenseService)
		{
			_userManager = userManager;
			_guestLicenseService = guestLicenseService;
		}

		[HttpPost("Create")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<int>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Create([FromBody] CreateGuestLicenseViewModel Model)
		{

			var response = new GeneralResponse<int>();
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


			return Ok(_guestLicenseService.Create(Model, user));

		}

		[HttpPost("Update")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Update([FromBody] UpdateGuestLicenseViewModel Model)
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


			return Ok(_guestLicenseService.Update(Model, user));

		}

		[HttpGet("Get/{Id}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<GetGuestLicenseResponse>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Get(int Id)
		{

			var response = new GeneralResponse<GetGuestLicenseResponse>();
			System.Security.Claims.ClaimsPrincipal currentUser = this.User;
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}
			return Ok(_guestLicenseService.Get(Id, user));

		}

		[HttpGet("GetAll/{AssociationId}/{Page}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<GeneralPaginationModel<GetListGuestLicenseResponse>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetAll(int AssociationId, int Page)
		{

			var response = new GeneralResponse<GeneralPaginationModel<GetListGuestLicenseResponse>>();
			System.Security.Claims.ClaimsPrincipal currentUser = this.User;
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}
			return Ok(_guestLicenseService.GetAll(Page, user,AssociationId));

		}


		[HttpPost("ChangeStatus")]
		[Authorize(Roles ="SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusGuestLicenseViewModel Model)
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
			return Ok(_guestLicenseService.ChangeStatus(Model.GuestLicenseId, Model.StatusId));

		}


	}
}
