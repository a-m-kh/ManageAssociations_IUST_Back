using Logic.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Services.Interface
{
	public interface IBaseInfoService
	{
		GeneralResponse<List<BaseInfoResponse>> GetBaseInfo(int parrentId);
	}
}
