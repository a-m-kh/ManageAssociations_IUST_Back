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
	public class JournalController : Controller
	{
		private readonly IWebHostEnvironment _environment;
		private readonly IJournalService _journalService;
		private readonly UserManager<User> _userManager;

		public JournalController(IWebHostEnvironment environment, IJournalService journalService, UserManager<User> userManager)
		{
			_environment = environment;
			_journalService = journalService;
			_userManager = userManager;
		}

		[HttpGet("Get/{Id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<GetJournalResponse>))]
		[ProducesDefaultResponseType]
		public IActionResult Get(int Id)
		{
			return (Ok(_journalService.Get(Id)));
		}

		[HttpGet("GetAll/{AssociationId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<GetJournalResponse>>))]
		[ProducesDefaultResponseType]
		public IActionResult GetAll(int AssociationId)
		{
			return (Ok(_journalService.GetAll(AssociationId)));
		}


		[HttpPost("Create")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesDefaultResponseType]
		[Authorize]
		public async Task<IActionResult> Create([FromForm] CreateJournalViewModel Vm)
		{
			var response = new GeneralResponse<List<GetJournalResponse>>();
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


			return Ok(_journalService.Create(Vm, user, _environment.WebRootPath));
		}

		[HttpPost("Update")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesDefaultResponseType]
		[Authorize]
		public async Task<IActionResult> Update([FromForm] UpdateJournalViewModel Vm)
		{
			var response = new GeneralResponse<List<GetJournalResponse>>();
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


			return Ok(_journalService.Update(Vm, user, _environment.WebRootPath));
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


			return Ok(_journalService.Delete(Id, user, _environment.WebRootPath));
		}
	}
}
