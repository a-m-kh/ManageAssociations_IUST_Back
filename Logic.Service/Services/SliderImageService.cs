using AutoMapper;
using DataBase.Configuration.Domain;
using DataBase.Repository.Repositories.Interface;
using Logic.Service.Responses;
using Logic.Service.Services.Interface;
using Logic.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Service.Services
{
	public class SliderImageService : ISliderImageService
	{

		private readonly ISliderImageRepository _sliderImageRepository;
		private readonly IMapper _mapper;
		public SliderImageService(ISliderImageRepository sliderImageRepository, IMapper mapper)
		{
			_sliderImageRepository = sliderImageRepository;
			_mapper = mapper;
		}

		public GeneralResponse<int> Create(CreateSliderImageViewModel vm, string WrPath)
		{
			var res = new GeneralResponse<int>();
			string imageUrl = null;
			if (vm.Image != null)
			{
				var upload = GeneralFunctions.Upload(vm.Image, "Slider", WrPath, "Images/Sliders");
				if (upload.Item2)
					imageUrl = upload.Item1;
			}
			if(imageUrl!= null)
			{
				var id = _sliderImageRepository.Create(imageUrl);
				res.Data = id;
				return res;
			}
			res.IsSuccess = false;
			res.Message = "مشکلی پیش آمده، لطفا مجددا اقدام نمایید.";
			return res;

		}

		public GeneralResponse<bool> Delete(int Id, string WrPath)
		{
			var res = new GeneralResponse<bool>();
			var entity = _sliderImageRepository.Get(Id);
			
			if(entity == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین عکسی وجود ندارد.";
				return res;
			}
			////////////////// delete image
			if (entity.ImageUrl != null)
			{
				GeneralFunctions.DeleteImage($@"{WrPath}\{entity.ImageUrl}");
			}
			//////
			

			res.IsSuccess = _sliderImageRepository.Delete(Id);
			res.Data = _sliderImageRepository.Delete(Id); 
			return res;
		}

		public GeneralResponse<List<SliderImage>> GetAllForAdmin()
		{
			var res = new GeneralResponse<List<SliderImage>>();
			res.Data = _sliderImageRepository.GetAll();
			return res;
		}

		public GeneralResponse<List<GetForUserSliderImageResponse>> GetAll()
		{
			var res = new GeneralResponse<List<GetForUserSliderImageResponse>>();
			res.Data = _mapper.Map<List<GetForUserSliderImageResponse>>(_sliderImageRepository.GetAll());
			return res;
		}
	}
}
