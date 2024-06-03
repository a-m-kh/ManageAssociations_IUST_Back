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
	public class NewsController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly IWebHostEnvironment _environment;
		private readonly INewsService _newsService;


		public NewsController(UserManager<User> userManager, IWebHostEnvironment environment, INewsService newsService)
		{
			_userManager = userManager;
			_environment = environment;
			_newsService = newsService;
		}

		[HttpPost("Create")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<int>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Create([FromForm] CreateNewsViewModel Model)
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


			return Ok(_newsService.Create(Model, user, _environment.WebRootPath));

		}

		[HttpPost("Update")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Create([FromBody] UpdateNewsViewModel Model)
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


			return Ok(_newsService.Update(Model, user, _environment.WebRootPath));

		}

		[HttpGet("Get/{Id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<LoginResponse>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Get(int Id)
		{

			var response = new GeneralResponse<LoginResponse>();
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


			return Ok(_newsService.Get(Id));

		}



		[HttpGet("GetAll")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<GetNewsResponse>>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetAll()
		{

			var response = new GeneralResponse<List<GetNewsResponse>>();
			return Ok(_newsService.GetAll());

		}





		[HttpGet("GetAllForAdmin/{AssociationId}/{Page}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<LoginResponse>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetAllForAdmin(int AssociationId,int Page)
		{

			var response = new GeneralResponse<GeneralPaginationModel<GetNewsForAdminResponse>>();
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


			return Ok(_newsService.GetForAdmin(Page, AssociationId,user));
		}

		[HttpPost("ChangeStatus")]
		[Authorize(Roles ="SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusNewsViewModel Model)
		{

			var response = new GeneralResponse<LoginResponse>();
			if (!ModelState.IsValid)
			{
				var errors = string.Join(" | ", ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage));
				response.IsSuccess = false;
				response.Message = errors;
				return Ok(response);
			}
			return Ok(_newsService.ChangeStatus(Model.NewsId,Model.StatusId));


		}
		[HttpPost("ChangeActive")]
		[Authorize(Roles ="SuperAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> ChangeActive([FromBody] ChangeActiveNewsViewModel Model)
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

			return Ok(_newsService.ChangePublic(Model.NewsId));
		}

		[HttpDelete("Delete/{NewsId}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Delete(int NewsId)
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

			return(Ok(_newsService.Delete(NewsId, user, _environment.WebRootPath)));

		}

		[HttpDelete("DeleteImage/{NewsId}/{ImageId}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> DeleteImage(int NewsId,int ImageId)
		{

			var response = new GeneralResponse<LoginResponse>();
			if (!ModelState.IsValid)
			{
				var errors = string.Join(" | ", ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage));
				response.IsSuccess = false;
				response.Message = errors;
				return Ok(response);
			}
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}

			return (Ok(_newsService.DeleteImage(NewsId, user,ImageId, _environment.WebRootPath)));

		}

		[HttpPost("AddImage")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<bool>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> AddImage([FromForm] AddImageNewsViewModel Model)
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
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				response.IsSuccess = false;
				response.Message = "همچین کاربری یافت نشد";
				return Ok(response);
			}

			return (Ok(_newsService.AddImage(Model.NewsId, Model.Image,user, _environment.WebRootPath)));

		}

	}
}
