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
	public class FormController : Controller
	{

		private readonly IWebHostEnvironment _environment;
		private readonly IFormService _FormService;

		public FormController(IWebHostEnvironment environment, IFormService formService)
		{
			_environment = environment;
			_FormService = formService;
		}

		[HttpPost("Create")]
		[Authorize(Roles ="SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<int>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Create([FromForm] FormViewModel VM)
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
			


			return Ok(_FormService.Create(VM, _environment.WebRootPath));

		}


		[HttpDelete("Delete/{FormId}")]
		[Authorize(Roles = "SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Delete(int FormId)
		{
			return Ok(_FormService.Delete(FormId, _environment.WebRootPath));

		}


		[HttpPost("Update")]
		[Authorize(Roles = "SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Update([FromForm] UpdateFormViewModel VM)
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



			return Ok(_FormService.Update(VM, _environment.WebRootPath));

		}


		[HttpGet("GetAll")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<FormGetDto>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_FormService.GetAll());
		}
	}
}
