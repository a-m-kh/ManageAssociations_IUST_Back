using Logic.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Logic.Service.Services;

public class AccountService:IAccountService
{
	private readonly UserManager<User> _UserManage;

	public AccountService(UserManager<User> userManage)
	{
		_UserManage = userManage;
	}
	public Task<GeneralResponse<LoginResponse>> Login(LoginViewModel VModel)
	{
		throw new NotImplementedException();
	}

	public Task<GeneralResponse<SignUpResponse>> SignUp(SignUpViewModel VModel)
	{
		throw new NotImplementedException();
	}
}
