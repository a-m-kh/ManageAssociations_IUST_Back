using Logic.Service.Responses;
using Logic.Service.Services;
using Logic.Service.Services.Interface;
using Logic.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebApi_BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseInfoController : Controller
{
	private readonly IBaseInfoService _baseInfoService;
	public BaseInfoController(IBaseInfoService baseInfoService)
	{
		_baseInfoService = baseInfoService;
	}

	[HttpGet("Get/{parrentId}")]
	[Authorize]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResponse<List<BaseInfoResponse>>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesDefaultResponseType]
	public async Task<IActionResult> Get(int parrentId)
	{
		return Ok(_baseInfoService.GetBaseInfo(parrentId));
	}
}
