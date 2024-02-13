using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Service.Responses;
using Logic.Service.ViewModels;

namespace Logic.Service.Services.Interface
{
	public interface IAssociationService
	{
		Task<GeneralResponse<GetAssociationResponse>> GetByIdAsync(int id);
		Task<GeneralResponse<bool>> Update(UpdateAssociationViewModel VModel, string WrPath);
		Task<GeneralResponse<int>> CreateAsync(CreateAssociationViewModel VModel, string WrPath);
		Task<GeneralResponse<bool>> DeleteByIdAsync(int id);
		Task<GeneralResponse<List<GetAssociationResponse>>> GetAll();
	}
}
