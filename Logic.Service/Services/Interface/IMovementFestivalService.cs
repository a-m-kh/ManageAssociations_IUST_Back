using Logic.Service.Responses;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Dtos;

namespace Logic.Service.Services.Interface
{
	public interface IMovementFestivalService
	{
		public GeneralResponse<int> CreateMovementFestival();
		GeneralResponse<int> CreateCompetitiveField(int MovementFestival);
		GeneralResponse<int> CreatePosition(CreatePositionViewModel vm);
		GeneralResponse<bool> DeletePositon(int Id);
		GeneralResponse<bool> DeleteCompetitiveField(int Id);
		GeneralResponse<bool> DeleteMovementFestival(int Id);
		GeneralResponse<bool> UpdatePosition(UpdatePositionViewModel vm);
		GeneralResponse<GetForAdminMovementFestivalDto> GetForAdmin(int Id);
		GeneralResponse<List<GetForAdminMovementFestivalDto>> GetAllForAdmin();
	}
}
