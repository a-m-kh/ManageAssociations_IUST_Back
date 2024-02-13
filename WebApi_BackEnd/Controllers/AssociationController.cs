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
	public class AssociationController : Controller
	{
		private readonly IWebHostEnvironment _environment;
		private readonly IAssociationService _associationService;

		public AssociationController(IWebHostEnvironment environment, IAssociationService associationService)
		{
			_environment = environment;
			_associationService = associationService;
		}

		[HttpPost("Create")]
		[Authorize(Roles ="SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Create([FromForm] CreateAssociationViewModel Model)
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
			return Ok(await _associationService.CreateAsync(Model, _environment.WebRootPath));
		}

		[HttpPost("Update")]
		[Authorize(Roles = "SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Update([FromForm] UpdateAssociationViewModel Model)
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
			return Ok(await _associationService.Update(Model, _environment.WebRootPath));
		}

		[HttpGet("Get/{id}")]
		[Authorize(Roles = "SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<GetAssociationResponse>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Get(int id)
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
			return Ok(await _associationService.GetByIdAsync(id));
		}

		[HttpDelete("Delete/{id}")]
		[Authorize(Roles = "SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<GetAssociationResponse>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Delete(int id)
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
			return Ok(await _associationService.DeleteByIdAsync(id));
		}



		[HttpGet("GetAll")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<GetAssociationResponse>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetAll()
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
			return Ok(await _associationService.GetAll());
		}
	}
}
