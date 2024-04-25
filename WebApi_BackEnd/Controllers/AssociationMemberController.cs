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
	public class AssociationMemberController : Controller
	{
		private readonly IAssociationMemberService _associationMemberService;
		private readonly UserManager<User> _userManager;
		private readonly IWebHostEnvironment _environment;

		public AssociationMemberController(IAssociationMemberService associationMemberService, UserManager<User> userManager, IWebHostEnvironment environment)
		{
			_associationMemberService = associationMemberService;
			_userManager = userManager;
			_environment = environment;
		}



		[HttpGet("Get/{Id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<GetAssociationMemberResponse>))]
		[ProducesDefaultResponseType]
		public IActionResult Get(int Id)
		{
			return (Ok(_associationMemberService.Get(Id)));
		}

		[HttpGet("GetAll/{AssociationId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<GetAssociationMemberResponse>>))]
		[ProducesDefaultResponseType]
		public IActionResult GetAll(int AssociationId)
		{
			return (Ok(_associationMemberService.GetAll(AssociationId)));
		}


		[HttpPost("Create")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<int>))]
		[ProducesDefaultResponseType]
		[Authorize]
		public async Task<IActionResult> Create([FromForm] CreateAssociationMemberViewModel Vm)
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


			return Ok(_associationMemberService.Create(Vm, user, _environment.WebRootPath));
		}

		[HttpPost("Update")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesDefaultResponseType]
		[Authorize]
		public async Task<IActionResult> Update([FromForm] UpdateAssociationMemberViewModel Vm)
		{
			var response = new GeneralResponse<List<GetAssociationMemberResponse>>();
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


			return Ok(_associationMemberService.Update(Vm, user, _environment.WebRootPath));
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


			return Ok(_associationMemberService.Delete(Id, user, _environment.WebRootPath));
		}
	}
}
