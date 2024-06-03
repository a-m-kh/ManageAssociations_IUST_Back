using Logic.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using DataBase.Repository.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories;
using Utility;
using System.IO;
using Utility.Enums;

namespace Logic.Service.Services
{
	public class NewsService : INewsService
	{
		private readonly INewsRepository _newsRepository;
		private readonly IImageNewsRepository _imageNewsRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly IAssociationRepository _associationRepository;


		public NewsService(
			INewsRepository newsRepository,
			IImageNewsRepository imageNewsRepository,
			IMapper mapper,
			UserManager<User> userManager,
			IAssociationRepository associationRepository)
		{
			_newsRepository = newsRepository;
			_imageNewsRepository = imageNewsRepository;
			_mapper = mapper;
			_userManager = userManager;
			_associationRepository = associationRepository;
		}

		public GeneralResponse<int> Create(CreateNewsViewModel Model, User user, string WrPath)
		{
			var res = new GeneralResponse<int>()
			{
				IsSuccess = false
			};
			var associationDto = _associationRepository.Get(Model.AssociationId);
			if (associationDto == null)
			{
				res.Message = "همچین انجمنی وجود ندارد";
				return res;
			}
			var statusOfUser = GeneralFunctions.CheckPermission(Model.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			/*if (eventDto.ReportDto.Id != 0)
			{
				res.Message = "هر رویداد فقط یک گزارش میتواند داشته باشد.";
				return res;
			}*/
			var entity = _mapper.Map<CreateNewsDto>(Model);
			entity.StatusId = (int)BaseInfoEnum.Waiting;
			var entityId = _newsRepository.Create(entity);
			if (entityId == 0)
			{
				res.Message = "مشکلی پیش آمده، لطفا مجددا اقدام نمایید";
				return res;
			}
			var uploadImages = GeneralFunctions.UploadImage_Newss(Model.Images, "News", WrPath, "Images/News", entityId);
			if (uploadImages.Item2)
			{
				var imagesStatus = _imageNewsRepository.Create(uploadImages.Item3);
				if (imagesStatus)
				{
					res.IsSuccess = true;
					return res;
				}
				res.Message = "عکس ها ذخیره نشدند. عکس ها را دوباره آپلود کنید ";
				return res;
			}
			res.Message = "عکس ها ذخیره نشدند. عکس ها را دوباره آپلود کنید ";
			return res;
		}

		public GeneralResponse<bool> Update(UpdateNewsViewModel Model, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var newsDto = _newsRepository.Get(Model.Id);
			if (newsDto == null)
			{
				res.Message = "همچین خبری وجود ندارد";
				return res;
			}
			var statusOfUser = GeneralFunctions.CheckPermission(newsDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			var entity = _mapper.Map<UpdateNewsDto>(Model);
			var isUpdate = _newsRepository.Update(entity);
			if (isUpdate)
			{
				res.IsSuccess = true;
				return res;
			}
			return res;
		}

		public GeneralResponse<bool> Delete(int newsId, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var newsDto = _newsRepository.Get(newsId);

			if (newsDto == null)
			{
				res.Message = "همچین خبری وجود ندارد";
				return res;
			}

			var statusOfUser = GeneralFunctions.CheckPermission(newsDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			var imageStatus = _imageNewsRepository.DeleteAll(newsId);
			foreach (var image in newsDto.Images)
			{
				GeneralFunctions.DeleteImage($@"{WrPath}\{image.Url}");
			}

			//GeneralFunctions.DeleteImage("")
			if (imageStatus)
			{
				var reportStatus = _newsRepository.Delete(newsId);
				if (reportStatus)
				{
					res.IsSuccess = true;
					return res;
				}
			}
			res.Message = "مشکلی پیش امده، مجددا اقدام نمایید.";
			return res;
		}

		public GeneralResponse<GetNewsResponse> Get(int newsId)
		{
			var res = new GeneralResponse<GetNewsResponse>()
			{
				IsSuccess = false
			};
			var newsDto = _newsRepository.Get(newsId);

			if (newsDto == null)
			{
				res.Message = "همچین خبری وجود ندارد";
				return res;
			}

			var data = _mapper.Map<GetNewsResponse>(newsDto);
			res.IsSuccess = true;
			res.Data = data;
			return res;
		}




		public GeneralResponse<List<GetNewsResponse>> GetAll()
		{
			var res = new GeneralResponse<List<GetNewsResponse>>()
			{
				IsSuccess = false
			};
			var newsDto = _newsRepository.GetAll();
			var data = _mapper.Map<List<GetNewsResponse>>(newsDto);
			res.IsSuccess = true;
			res.Data = data;
			return res;
		}




		public GeneralResponse<GeneralPaginationModel<GetNewsForAdminResponse>> GetForAdmin(int Page,int AssociationId ,User user)
		{
			var res = new GeneralResponse<GeneralPaginationModel<GetNewsForAdminResponse>>()
			{
				IsSuccess = false
			};
			var statusOfUser = GeneralFunctions.CheckPermission(AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			var PageData =_mapper.Map<GeneralPaginationModel<GetNewsForAdminResponse>>( _newsRepository.GetForAdmin(Page, AssociationId));

			
			res.IsSuccess = true;
			res.Data = PageData;
			return res;
		}

		public GeneralResponse<bool> ChangeStatus(int Id, int StatusId)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};

			var status = _newsRepository.ChangeStatus(Id,StatusId);
			if (status)
			{
				res.IsSuccess = true;
				return res;
			}

			res.Message = "مشکلی پیش آمده، لطفا مجددا اقدام نمایید.";
			return res;

		}

		public GeneralResponse<bool> ChangePublic(int Id)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};

			var status = _newsRepository.ChangePublic(Id);
			if (status)
			{
				res.IsSuccess = true;
				return res;
			}

			res.Message = "مشکلی پیش آمده، لطفا مجددا اقدام نمایید.";
			return res;
		}

		public GeneralResponse<bool> AddImage(int NewsId, IFormFile image, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var newsDto = _newsRepository.Get(NewsId);
			if (newsDto == null)
			{
				res.Message = "همچین خبری وجود ندارد";
				return res;
			}
			var statusOfUser = GeneralFunctions.CheckPermission(newsDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			var url = GeneralFunctions.Upload(image, "News", WrPath, "Images/News");
			if (url.Item2)
			{
				var status = _imageNewsRepository.Create(new NewsImage()
				{
					NewsId = newsDto.Id,
					Url = url.Item1
				});
				if (status)
				{
					res.IsSuccess = true;
					return res;
				}
			}
			res.Message = "مشکلی پیش آمده لطفا مجددا اقدام نمایید";
			return res;
		}

		public GeneralResponse<bool> DeleteImage(int NewsId, User user, int ImageId,string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};
			var imageDto = _imageNewsRepository.GetImage(ImageId);
			if (imageDto == null)
			{
				res.Message = "همچین عکسی وجود ندارد";
				return res;
			}
			var newsDto = _newsRepository.Get(NewsId);
			var statusOfUser = GeneralFunctions.CheckPermission(newsDto.AssociationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			GeneralFunctions.DeleteImage($@"{WrPath}\{imageDto.Url}");
			if (_imageNewsRepository.Delete(ImageId))
			{
				res.IsSuccess = true;
				return res;
			}
			res.Message = "مشکلی پیش امده، مجددا اقدام نمایید.";
			return res;
		}
	}
}
