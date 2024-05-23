using DataBase.Configuration.Domain;
using Logic.Service.Responses;
using Logic.Service.Services;
using Logic.Service.Services.Interface;
using Logic.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebApi_BackEnd.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SliderImageController : Controller
	{
		private readonly IWebHostEnvironment _environment;
		private readonly ISliderImageService _sliderImageService;

		public SliderImageController(IWebHostEnvironment environment, ISliderImageService sliderImageService)
		{
			_environment = environment;
			_sliderImageService = sliderImageService;
		}

		[HttpPost("Create")]
		[Authorize(Roles = "SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<int>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public IActionResult Create([FromForm] CreateSliderImageViewModel vm)
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
			return Ok(_sliderImageService.Create(vm, _environment.WebRootPath));
		}

		[HttpDelete("Delete/{SliderImageId}")]
		[Authorize(Roles = "SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public IActionResult Delete(int SliderImageId)
		{
			var response = new GeneralResponse<bool>();
			return Ok(_sliderImageService.Delete(SliderImageId, _environment.WebRootPath));
		}


		[HttpGet("GetAllForAdmin")]
		[Authorize(Roles = "SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<SliderImage>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public IActionResult GetAllForAdmin()
		{
			var response = new GeneralResponse<List<SliderImage>>();
			return Ok(_sliderImageService.GetAllForAdmin());
		}

		[HttpGet("GetAllForUser")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<GetForUserSliderImageResponse>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public IActionResult GetAllForUser()
		{
			var response = new GeneralResponse<List<GetForUserSliderImageResponse>>();
			return Ok(_sliderImageService.GetAll());
		}
	}
}
