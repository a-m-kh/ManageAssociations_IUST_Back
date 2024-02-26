using Logic.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Service.Responses;
using DataBase.Repository.Repositories.Interface;
using AutoMapper;

namespace Logic.Service.Services;

public class BaseInfoService : IBaseInfoService
{
	private readonly IBaseInfoRepository _baseInfoRepository;
	private readonly IMapper _mapper;

	public BaseInfoService(IBaseInfoRepository baseInfoRepository, IMapper mapper)
	{
		_baseInfoRepository = baseInfoRepository;
		_mapper = mapper;
	}
	public GeneralResponse<List<BaseInfoResponse>> GetBaseInfo(int parrentId)
	{
		var res = new GeneralResponse<List<BaseInfoResponse>>();
		var result = _baseInfoRepository.Get(parrentId);
		if(result == null)
		{
			res.IsSuccess = false;
			res.Message = "همچین آیدی یافت نشد";
			return res;
		}
		var data = _mapper.Map<List<BaseInfoResponse>>(result);
		res.Data = data;
		return res;
	}
}
