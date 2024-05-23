using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories.Interface;
using Logic.Service.Responses;
using Logic.Service.Services.Interface;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Services
{
	public class MovementFestivalService : IMovementFestivalService
	{
		private readonly IMovementFestivalRepository _movementFestivalRepository;
		private readonly ICompetitiveFieldRepository _competitiveFieldRepository;
		private readonly IPositionRepository _positionRepository;


		public MovementFestivalService(
			IMovementFestivalRepository movementFestivalRepository,
			ICompetitiveFieldRepository competitiveFieldRepository,
			IPositionRepository positionRepository
			)
		{
			_movementFestivalRepository = movementFestivalRepository;
			_competitiveFieldRepository = competitiveFieldRepository;
			_positionRepository = positionRepository;
		}


		public GeneralResponse<int> CreateMovementFestival()
		{
			var response = new GeneralResponse<int>();
			var id= _movementFestivalRepository.Create();
			response.IsSuccess = id > 0;
			response.Data = id;
			return response;
		}

		public GeneralResponse<int> CreateCompetitiveField(int MovementFestival)
		{
			var response = new GeneralResponse<int>();
			if (!_movementFestivalRepository.Exist(MovementFestival))
			{
				response.IsSuccess = false;
				response.Message = "همچین جشنواره حرکتی وجود ندارد.";
				return response;
			}
			var id = _competitiveFieldRepository.Create(MovementFestival);
			response.IsSuccess = id > 0;
			response.Data = id;
			return response;
		}

		public GeneralResponse<int> CreatePosition(CreatePositionViewModel vm)
		{
			var response = new GeneralResponse<int>();
			if (!_competitiveFieldRepository.Exist(vm.CompetiveFieldId))
			{
				response.IsSuccess = false;
				response.Message = "همچین حوزه رقابتی وجود ندارد.";
				return response;
			}
			var id = _positionRepository.Create(vm.CompetiveFieldId , vm.Owner, vm.TitleId);
			response.IsSuccess = id > 0;
			response.Data = id;
			return response;
		}

		public GeneralResponse<bool> DeletePositon(int Id)
		{
			var res = new GeneralResponse<bool>();
			var isDelete = _positionRepository.Delete(Id);
			res.IsSuccess = isDelete;
			res.Data = isDelete;
			return res;
		}

		public GeneralResponse<bool> DeleteCompetitiveField(int Id)
		{
			var res = new GeneralResponse<bool>();
			var isDelete = _competitiveFieldRepository.Delete(Id);
			res.IsSuccess = isDelete;
			res.Data = isDelete;
			return res;
		}

		public GeneralResponse<bool> DeleteMovementFestival(int Id)
		{
			var res = new GeneralResponse<bool>();
			var isDelete = _movementFestivalRepository.Delete(Id);
			res.IsSuccess = isDelete;
			res.Data = isDelete;
			return res;
		}

		public GeneralResponse<bool> UpdatePosition(UpdatePositionViewModel vm)
		{
			var res = new GeneralResponse<bool>();
			var isupdate = _positionRepository.Update(vm.Owner, vm.TitleId, vm.Id);
			res.IsSuccess = isupdate;
			res.Data = isupdate;
			return res;
		}

		public GeneralResponse<GetForAdminMovementFestivalDto> GetForAdmin(int Id)
		{
			var res = new GeneralResponse<GetForAdminMovementFestivalDto>();
			res.Data = _movementFestivalRepository.Get(Id);
			return res;
		}

		public GeneralResponse<List<GetForAdminMovementFestivalDto>> GetAllForAdmin()
		{
			var res = new GeneralResponse<List<GetForAdminMovementFestivalDto>>();
			res.Data = _movementFestivalRepository.GetAll();
			return res;
		}
	}
}
