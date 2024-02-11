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
	public class EventController : Controller
	{
		private readonly IEventService _eventService;
		private readonly UserManager<User> _userManager;

		public EventController(IEventService eventService, UserManager<User> userManager)
		{
			_eventService = eventService;
			_userManager = userManager;
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
			var userId = _userManager.GetUserId(User);

			if (userId == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}
			return Ok(await _eventService.CreateEventsAsync(Model ,userId));
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
			var userId = _userManager.GetUserId(User);
			if (userId == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}
			return Ok(await _eventService.UpdateEventsAsync(Model, userId));
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
			var userId = _userManager.GetUserId(User);
			if (userId == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}
			return Ok(await _eventService.DeleteEventsAsync(EventId , userId));
		}

		[HttpGet("Get/{EventId}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
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

	}
}
