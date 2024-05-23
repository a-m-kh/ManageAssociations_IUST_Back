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
	public interface ISliderImageService
	{
		GeneralResponse<int> Create(CreateSliderImageViewModel vm, string WrPath);
		GeneralResponse<bool> Delete(int Id, string WrPath);
		GeneralResponse<List<SliderImage>> GetAllForAdmin();
		GeneralResponse<List<GetForUserSliderImageResponse>> GetAll();
	}
}
