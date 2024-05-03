using DataBase.Configuration.Domain;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Service.Responses;
using Microsoft.AspNetCore.Http;
using DataBase.Configuration.Dtos;

namespace Logic.Service.Services.Interface
{
	public interface INewsService
	{
		GeneralResponse<int> Create(CreateNewsViewModel Model, User user, string WrPath);
		GeneralResponse<bool> Update(UpdateNewsViewModel Model, User user, string WrPath);
		GeneralResponse<bool> Delete(int Id,User user ,string WrPath);
		GeneralResponse<GetNewsResponse> Get(int Id);
		GeneralResponse<GeneralPaginationModel<GetNewsForAdminResponse>> GetForAdmin(int Page,int AssociationId,  User user);
		GeneralResponse<bool> ChangeStatus(int Id, int StatusId);
		GeneralResponse<bool> ChangePublic(int Id);
		GeneralResponse<bool>AddImage(int NewsId,IFormFile image,User user,string WrPath);
		GeneralResponse<bool> DeleteImage(int NewsId, User user, int ImageId,string WrPath);
	}
}
