using DataBase.Configuration.Domain;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Services.Interface
{
	public interface IAssociationMemberService
	{
		GeneralResponse<int> Create(CreateAssociationMemberViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Update(UpdateAssociationMemberViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Delete(int id, User user, string WrPath);
		GeneralResponse<GetAssociationMemberResponse> Get(int id);
		GeneralResponse<List<GetAssociationMemberResponse>> GetAll(int AssociationId);
	}
}
