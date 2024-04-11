using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
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

		Task<GeneralResponse<int>> CreateCommunication(CreateCommunicationViewModel VModel, User user);
		Task<GeneralResponse<bool>> UpdateCommunication(UpdateCommunicationViewModel VModel, User user);
		GeneralResponse<bool> DeleteCommunication(int Id, User user);
		GeneralResponse<GetCommunicationResponse> GetCommunication(int Id);
		GeneralResponse<List<GetCommunicationResponse>> GetAllCommunication(int Id);

	}
}
