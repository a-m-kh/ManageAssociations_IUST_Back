using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface INewsRepository
	{
		int Create(CreateNewsDto Model);
		bool Update(UpdateNewsDto Model);
		bool Delete(int Id);
		GetNewsDto Get(int Id);
	    GeneralPaginationModel<GetNewsDto> GetForAdmin(int Page, int AssociationId);
		bool ChangeStatus(int Id, int StatusId);
		bool ChangePublic(int Id);
		GetNewsDto Get_NotViews(int Id);


	}
}
