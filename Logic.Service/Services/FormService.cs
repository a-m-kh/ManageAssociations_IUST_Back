using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories.Interface;
using Logic.Service.Responses;
using Logic.Service.Services.Interface;
using Logic.Service.ViewModels;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Service.Services
{
	public class FormService: IFormService
	{
		private readonly IFormRepository _formRepository;

		public FormService(IFormRepository formRepository)
		{
			_formRepository = formRepository;
		}

		public GeneralResponse<List<Form>> GetAll()
		{
			var res = new GeneralResponse<List<Form>>();
			res.Data = _formRepository.GetAll();
			return res;
		}

		public GeneralResponse<bool> Update(UpdateFormViewModel vm, string WrPath)
		{
			var res = new GeneralResponse<bool>();

			var enitity = _formRepository.Find(a => a.ID == vm.Id);


			////////////////// delete pdf
			if (enitity.Url != null && vm.Form != null)
			{
				//var url = WrPath + @"\";
				GeneralFunctions.DeleteImage($@"{WrPath}\{enitity.Url}");
			}
			///////////////////////////////////////////////////////////
			///

			//////////////// Upload New pdf
			string pdfUrl = null;
			if (vm.Form != null)
			{
				var upload = GeneralFunctions.Upload(vm.Form, "Form", WrPath, "Pdfs/Forms");
				if (upload.Item2)
					pdfUrl = upload.Item1;
			}

			/////////////////////////////////
			///

			var fDto = new FormUpdateDto()
			{
				Id = vm.Id,
				Title = vm.Title,
				Url = pdfUrl,
			};
			var isUpdate = _formRepository.Update(fDto);
			res.IsSuccess = isUpdate;
			res.Message = isUpdate == true ? ("") : ("مشکلی پیش آمده لطفا مجددا اقدام نمایید.");
			return res;
		}

		public GeneralResponse<int> Create(FormViewModel vm, string WrPath)
		{
			var res = new GeneralResponse<int>();
			string pdfUrl = null;
			if (vm.Form != null)
			{
				var upload = GeneralFunctions.Upload(vm.Form, "Form", WrPath, "Pdfs/Forms");
				if (upload.Item2)
					pdfUrl = upload.Item1;
			}
			var entityId = _formRepository.Create(pdfUrl, vm.Title);
			if(entityId > 0)
			{
				res.Data = entityId;
				return res;
			}
			res.IsSuccess = false;
			res.Message = "مشکلی پیش آمده؛ لطفا مجددا اقدام نمایید.";
			return res;
		}


		public GeneralResponse<bool>Delete(int Id,string WrPath)
		{
			var res = new GeneralResponse<bool>();

			var enitity = _formRepository.Find(a => a.ID == Id);


			////////////////// delete pdf
			if (enitity.Url != null)
			{
				//var url = WrPath + @"\";
				GeneralFunctions.DeleteImage($@"{WrPath}\{enitity.Url}");
			}
			///////////////////////////////////////////////////////////
			///

			var result = _formRepository.Delete(Id);
			res.IsSuccess = result;
			res.Message = result == false ? ("مشکلی پیش آمده، لطفا مجددا اقدام نمایید.") : ("");
			return res;
		}
	}
}
