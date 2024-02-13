using Logic.Service.Responses;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Services.Interface;

public interface IAccountService
{
	Task<GeneralResponse<LoginResponse>> Login(LoginViewModel VModel);
	Task<GeneralResponse<SignUpResponse>>SignUp(SignUpViewModel VModel);
	Task<GeneralResponse<EditProfileResponse>> EditProfile(EditProfileViewModel VModel, string userId);
	Task<GeneralResponse<bool>> Assign(string userId, int AssociationId);
}
