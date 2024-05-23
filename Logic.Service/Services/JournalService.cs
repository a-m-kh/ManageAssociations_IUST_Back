using AutoMapper;
using DataBase.Configuration.Domain;
using DataBase.Repository.Repositories.Interface;
using Logic.Service.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories;
using Utility;

namespace Logic.Service.Services
{
	public class JournalService : IJournalService
	{
		private readonly IJournalRepository _journalRepository;
		private readonly IAssociationRepository _associationRepository;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;

		public JournalService(IJournalRepository journalRepository, IAssociationRepository associationRepository, UserManager<User> userManager, IMapper mapper)
		{
			_journalRepository = journalRepository;
			_associationRepository = associationRepository;
			_userManager = userManager;
			_mapper = mapper;
		}


		public GeneralResponse<int> Create(CreateJournalViewModel vm, User user, string WrPath)
		{
			var res = new GeneralResponse<int>()
			{
				IsSuccess = false
			};
			var association = _associationRepository.Get(vm.AssociationId);
			if (association == null)
			{
				res.Message = "همچین انجمنی وجود ندارد";
				return res;
			}
			var statusOfUser = GeneralFunctions.CheckPermission(association.ID, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}
			string pdfUrl = null;
			if (vm.Pdf != null)
			{
				var upload = GeneralFunctions.Upload(vm.Pdf, "Association_Journal", WrPath, "Pdfs/Journals");
				if (upload.Item2)
					pdfUrl = upload.Item1;
			}

			string imageUrl = null;
			if (vm.Image != null)
			{
				var upload = GeneralFunctions.Upload(vm.Image, "Association_Journal", WrPath, "Images/Journals");
				if (upload.Item2)
					imageUrl = upload.Item1;
			}


			var entity = _mapper.Map<CreateJournalDto>(vm);
			entity.PdfUrl = pdfUrl;
			entity.ImageUrl = imageUrl;
			var Id = _journalRepository.Create(entity);
			if (Id > 0)
			{
				res.IsSuccess = true;
				res.Data = Id;
				return res;
			}
			res.Message = "مشکلی پیش آمده است، لطفا مجددا اقدام نمایید";
			return res;
		}

		public GeneralResponse<bool> Update(UpdateJournalViewModel vm, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};

			var journalDto = _journalRepository.Get(vm.Id);
			if (journalDto == null)
			{
				res.Message = "همچین نشریه ای وجود ندارد";
				return res;
			}



			var statusOfUser = GeneralFunctions.CheckPermission(journalDto.AssoiciationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			////////////////// delete pdf
			if (journalDto.PdfUrl != null && vm.Pdf != null)
			{
				//var url = WrPath + @"\";
				GeneralFunctions.DeleteImage($@"{WrPath}\{journalDto.PdfUrl}");
			}
			///////////////////////////////////////////////////////////


			////////////////// delete pdf
			if (journalDto.ImageUrl != null && vm.Image != null)
			{
				//var url = WrPath + @"\";
				GeneralFunctions.DeleteImage($@"{WrPath}\{journalDto.ImageUrl}");
			}
			///////////////////////////////////////////////////////////



			//////////////// Upload New pdf
			string imageUrl = null;
			if (vm.Image != null)
			{
				var upload = GeneralFunctions.Upload(vm.Image, "Association_Journal", WrPath, "Images/Journals");
				if (upload.Item2)
					imageUrl = upload.Item1;
			}

			/////////////////////////////////



			//////////////// Upload New Image
			string pdfUrl = null;
			if (vm.Pdf != null)
			{
				var upload = GeneralFunctions.Upload(vm.Pdf, "Association_Journal", WrPath, "Pdfs/Journals");
				if (upload.Item2)
					pdfUrl = upload.Item1;
			}

			/////////////////////////////////



			var entity = _mapper.Map<UpdateJournalDto>(vm);
			journalDto.PdfUrl = pdfUrl;
			journalDto.ImageUrl = imageUrl;
			var status = _journalRepository.Update(entity);
			if (status)
			{
				res.IsSuccess = true;
				res.Data = status;
				return res;
			}
			res.Message = "مشکلی پیش آمده است، لطفا مجددا اقدام نمایید";
			return res;
		}

		public GeneralResponse<bool> Delete(int Id, User user, string WrPath)
		{
			var res = new GeneralResponse<bool>()
			{
				IsSuccess = false
			};

			var journalDto = _journalRepository.Get(Id);
			if (journalDto == null)
			{
				res.Message = "همچین نشریه ای وجود ندارد";
				return res;
			}



			var statusOfUser = GeneralFunctions.CheckPermission(journalDto.AssoiciationId, user, _userManager, _associationRepository);
			if (!statusOfUser.Item1)
			{
				res.Message = statusOfUser.Item2;
				return res;
			}

			////////////////// delete pdf
			if (journalDto.PdfUrl != null)
			{
				GeneralFunctions.DeleteImage($@"{WrPath}\{journalDto.PdfUrl}");
			}
			///////////////////////////////////////////////////////////
			///////////////////// delete image
			if (journalDto.ImageUrl != null)
			{
				GeneralFunctions.DeleteImage($@"{WrPath}\{journalDto.ImageUrl}");
			}
			///////////////////////////////////////////////////////////
			


			if (_journalRepository.Delete(Id))
			{
				res.IsSuccess = true;
				res.Data = true;
				return res;
			}
			res.Message = "مشکلی پیش امده است؛ لطفا مجددا اقدام نمایید.";
			return res;
		}

		public GeneralResponse<GetJournalResponse> Get(int id)
		{
			var res = new GeneralResponse<GetJournalResponse>();
			var entity = _journalRepository.Get(id);
			if (entity == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین نشریه ای وجود ندارد";
				return res;
			}
			res.Data = _mapper.Map<GetJournalResponse>(entity);
			return res;
		}

		public GeneralResponse<List<GetJournalResponse>> GetAll(int AssociationId)
		{
			var res = new GeneralResponse<List<GetJournalResponse>>();

			var entity = _journalRepository.GetAll(AssociationId);
			if (entity == null)
			{
				res.IsSuccess = false;
				res.Message = "همچین نشریه ای وجود ندارد";
				return res;
			}
			res.Data = _mapper.Map<List<GetJournalResponse>>(entity);
			return res;
		}
	}
}
