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
	public interface IJournalService
	{
		GeneralResponse<int> Create(CreateJournalViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Update(UpdateJournalViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Delete(int id, User user, string WrPath);
		GeneralResponse<GetJournalResponse> Get(int id);
		GeneralResponse<List<GetJournalResponse>> GetAll(int AssociationId);
	}
}
