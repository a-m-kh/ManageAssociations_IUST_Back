using Azure;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
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
	public class EventController : Controller
	{
		private readonly IEventService _eventService;
		private readonly UserManager<User> _userManager;
		private readonly IWebHostEnvironment _environment;

		public EventController(IEventService eventService, UserManager<User> userManager , IWebHostEnvironment environment)
		{
			_eventService = eventService;
			_userManager = userManager;
			_environment = environment;
		}

		[HttpPost("Create")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Create([FromForm] EventCreateViewModel Model)
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
			return Ok(await _eventService.CreateEventsAsync(Model, user, _environment.WebRootPath));
		}

		[HttpPost("Update")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Update([FromForm] EventUpdateViewModel Model)
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
			//var userId = _userManager.GetUserId(User);
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}
			return Ok(await _eventService.UpdateEventsAsync(Model, user, _environment.WebRootPath));
		}

		[HttpDelete("Delete/{EventId}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Delete(int EventId)
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
			return Ok(await _eventService.DeleteEventsAsync(EventId , user));
		}

		[HttpGet("Get/{EventId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<EventGetResponse>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Last_Events(int EventId)
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
			
			return Ok(await _eventService.GetEventsAsync(EventId));
		}



		[HttpGet("GetAllForUser/{AssociationId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<GetForUserEventDto>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetAllForUser(int AssociationId)
		{
			return Ok(await _eventService.GetAllForUser(AssociationId));
		}






		[HttpGet("GetAllForUser")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<GetForUserEventDto>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetAllForUser()
		{
			return Ok(await _eventService.GetAllForUser());
		}






		[HttpGet("GetAllForAdmin/{AssociationId}/{Page}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<GeneralPaginationModel<GetEventDto>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetAllForUser(int AssociationId, int Page)
		{
			var res = new GeneralResponse<GeneralPaginationModel<GetEventDto>>();

			System.Security.Claims.ClaimsPrincipal currentUser = this.User;
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین کاربری یافت نشد";
				return Ok(res);
			}


			return Ok(await _eventService.GetAllForAdmin(AssociationId,user, Page));
		}


		[HttpGet("PastEvent/{AssociationId}/{Page}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<GeneralPaginationModel<GetEventDto>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> PastEvent(int AssociationId, int Page)
		{
			var res = new GeneralResponse<GeneralPaginationModel<GetEventDto>>();

			System.Security.Claims.ClaimsPrincipal currentUser = this.User;
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین کاربری یافت نشد";
				return Ok(res);
			}


			return Ok(_eventService.GetAllPastEventt(AssociationId, Page));
		}







		[HttpGet("GetAllForSuperAdmin/{Page}")]
		[Authorize(Roles = "SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<GeneralPaginationModel<GetEventDto>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public IActionResult GetAllForSuperAdmin(int Page)
		{
			var res = new GeneralResponse<GeneralPaginationModel<GetEventDto>>();
			return Ok(_eventService.GetAllForSuperAdmin(Page));
		}


		[HttpPut("ChangeCofirm/{EventId}")]
		[Authorize(Roles = "SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public IActionResult ChangeCofirm([FromBody] EventChangeConfirmViewModel Vm)
		{
			var res = new GeneralResponse<bool>();
			return Ok(_eventService.ChangeConfirm(Vm));
		}

		[HttpPut("ChangePublic")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> ChangePublic([FromBody] EventChangePusblicViewModel Vm)
		{
			var res = new GeneralResponse<bool>();
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
			return Ok(_eventService.ChangePublic(Vm.Id,user, Vm.AssociationId));
		}


	}
}
