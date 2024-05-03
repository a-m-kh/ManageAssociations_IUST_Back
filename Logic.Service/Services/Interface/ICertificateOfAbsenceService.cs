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
	public interface ICertificateOfAbsenceService
	{
		GeneralResponse<int> Create(CreateCertificateOfAbsenceViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Update(UpdateCertificateOfAbsenceViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Delete(int Id, User user, string WrPath);
		GeneralResponse<GetCertificateOfAbsenceResponse> Get(int Id, User user);
		GeneralResponse<GeneralPaginationModel<GetCertificateOfAbsenceDto>> GetAll(int AssociationId, User user, int page = 1);
		GeneralResponse<bool> ChangeState(int CertificateId, int StatusId, string WrPath);
	}
}
