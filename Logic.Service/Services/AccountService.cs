using Logic.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Logic.Service.Services;

public class AccountService:IAccountService
{
	private readonly UserManager<User> _userManager;
	private readonly IConfiguration _configuration;

	public AccountService(UserManager<User> userManage,IConfiguration configuration)
	{
		_userManager = userManage;
		_configuration = configuration;
	}
	public async Task<GeneralResponse<LoginResponse>> Login(LoginViewModel VModel)
	{
		var response = new GeneralResponse<LoginResponse>();

		var user = await _userManager.FindByNameAsync(VModel.UserName);
		if(user != null && await _userManager.CheckPasswordAsync(user,VModel.Password))
		{
			var claims = _userManager.GetClaimsAsync(user).Result.ToList();
			var claimsDto = new List<UserClaimDto>();
			foreach (var claim in claims)
			{
				claimsDto.Add(new UserClaimDto()
				{
					ClaimType = claim.Type,
					ClaimValue = claim.Value
				});
			}
			var token = GenerateToken(user, claimsDto);
			response.Data = new LoginResponse()
			{
				UserName = VModel.UserName,
				Token = token
			};
			return response;
		}

		response.IsSuccess = false;
		response.Message = "نام کاربری یا رمزعبور اشتباه میباشد";
		return response;
	}

	private string GenerateToken(User user, List<UserClaimDto> userClaims)
	{
		var claims = new List<System.Security.Claims.Claim>
		{
			new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new System.Security.Claims.Claim(JwtRegisteredClaimNames.Name, user.UserName),
			new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
		};

		var roleClaims = userClaims.Select(c => new System.Security.Claims.Claim(c.ClaimType, c.ClaimValue));
		claims.AddRange(claims);
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings").GetSection("securityKey").Value));
		var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			claims: claims,
			expires: DateTime.Now.AddDays(30),
			signingCredentials: cred
		);
		return new JwtSecurityTokenHandler().WriteToken(token);

	}

	public async Task<GeneralResponse<SignUpResponse>> SignUp(SignUpViewModel VModel)
	{
		var res = new GeneralResponse<SignUpResponse>();
		var newUser = new User()
		{
			Id = Guid.NewGuid().ToString(),
			UserName = VModel.UserName,
		};
		try
		{
			var result = await _userManager.CreateAsync(newUser, VModel.Password);
			if (!result.Succeeded)
			{
				res.IsSuccess = false;
				res.Message = string.Join(" , ", result.Errors.Select(a => a.Description));
				return res;
			}
			res.Data = new SignUpResponse()
			{
				Username = newUser.UserName
			};
			return res;
		}catch(Exception ex)
		{
			var error = ex;
			return res;
		}
		

	}


	public async Task<GeneralResponse<EditProfileResponse>> EditProfile(EditProfileViewModel VModel,string userId)
	{
		var response = new GeneralResponse<EditProfileResponse>();

		var user = await _userManager.FindByIdAsync(userId);
		if(user != null)
		{
			user.PhoneNumber = VModel.PhoneNumber == null ? (user.PhoneNumber) : (VModel.PhoneNumber);
			user.Email = VModel.Email == null ?(user.Email):(VModel.Email);
			var isUpdate = await _userManager.UpdateAsync(user);
			if (isUpdate.Succeeded)
			{
				response.Data = new EditProfileResponse()
				{
					UserName = user.UserName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber
				};
				return response;
			}

			response.IsSuccess = false;
			response.Message = string.Join(" , ", isUpdate.Errors.Select(a => a.Description));
			return response;
		}
		response.IsSuccess = false;
		response.Message = "همچین کاربری پیدا نشد";
		return response;
	}
}
