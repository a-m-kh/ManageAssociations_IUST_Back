using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Services.Interface
{
	public interface ICertificationService
	{
		GeneralResponse<int> Create(CreateCertificationViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Update(UpdateCertificationViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Delete(int Id, User user, string WrPath);
		GeneralResponse<GetCertificationResponse> Get(int Id, User user);
		Task<GeneralResponse<GeneralPaginationModel<GetCertificationDto>>> GetAll(int AssociationId, User user, int page = 1);
		GeneralResponse<bool> ChangeState(int CertificateId, int StatusId, string WrPath);
		Task<GeneralResponse<GeneralPaginationModel<GetCertificationDto>>> GetAllForAdmin(User user, int page = 1);
	}
}
