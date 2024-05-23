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
	[Authorize(Roles = "SuperAdmin")]
	public class MovementFestivalController : Controller
	{

		private readonly IMovementFestivalService _movementFestivalService;

		public MovementFestivalController(IMovementFestivalService movementFestivalService)
		{
			_movementFestivalService = movementFestivalService;
		}


		[HttpPost("CreateMovementFestival")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<int>))]
		[ProducesDefaultResponseType]
		public  IActionResult CreateMovementFestival()
		{
			return Ok(_movementFestivalService.CreateMovementFestival());
		}

		[HttpPost("CreateCompetitiveField")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<int>))]
		[ProducesDefaultResponseType]
		public IActionResult CreateCompetitiveField([FromBody] CreateCompetitiveField vm)
		{
			return Ok(_movementFestivalService.CreateCompetitiveField(vm.MovementFestivalId));
		}
		[HttpPost("CreatePosition")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<int>))]
		[ProducesDefaultResponseType]
		public IActionResult CreatePosition([FromBody] CreatePositionViewModel vm)
		{
			return Ok(_movementFestivalService.CreatePosition(vm));
		}

		[HttpDelete("DeletePosition/{PositionId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesDefaultResponseType]
		public IActionResult DeletePosition(int PositionId)
		{
			return Ok(_movementFestivalService.DeletePositon(PositionId));
		}

		[HttpDelete("DeleteCompetitiveField/{CompetitiveFieldId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesDefaultResponseType]
		public IActionResult DeleteCompetitiveField(int CompetitiveFieldId)
		{
			return Ok(_movementFestivalService.DeleteCompetitiveField(CompetitiveFieldId));
		}

		[HttpDelete("DeleteMovementFestival/{MovementFestivalId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesDefaultResponseType]
		public IActionResult DeleteMovementFestival(int MovementFestivalId)
		{
			return Ok(_movementFestivalService.DeleteMovementFestival(MovementFestivalId));
		}


		[HttpGet("GetMovementFestival/{MovementFestivalId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<GetForAdminMovementFestivalDto>))]
		[ProducesDefaultResponseType]
		public IActionResult GetMovementFestival(int MovementFestivalId)
		{
			return Ok(_movementFestivalService.GetForAdmin(MovementFestivalId));
		}


		[HttpGet("GetAllMovementFestival")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<GetForAdminMovementFestivalDto>>))]
		[ProducesDefaultResponseType]
		public IActionResult GetAllMovementFestival()
		{
			return Ok(_movementFestivalService.GetAllForAdmin());
		}

	}
}
