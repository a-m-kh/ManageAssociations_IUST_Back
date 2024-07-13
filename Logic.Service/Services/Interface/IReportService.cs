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
	public interface IReportService
	{
		Task<GeneralResponse<int>> Create(CreateReportViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Update(UpdateReportViewModel vm, User user, string WrPath);
		GeneralResponse<bool> AddImage(AddImageViewModel vm, User user, string WrPath);
		GeneralResponse<bool> Delete(int ReportId, User user, string WrPath);
		GeneralResponse<bool> DeleteImage(int ImageId, User user, string WrPath);
		GeneralResponse<GetReportDto> GetReport(int ReportId, User user);
	}
}
