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
	public interface IFormService
	{
		GeneralResponse<List<Form>> GetAll();
		GeneralResponse<bool> Update(UpdateFormViewModel vm, string WrPath);
		GeneralResponse<int> Create(FormViewModel vm, string WrPath);
		GeneralResponse<bool> Delete(int Id, string WrPath);
	}
}
