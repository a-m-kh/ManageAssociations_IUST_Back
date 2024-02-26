using AutoMapper;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Mapper
{
	public class AutoMapper : Profile
	{
		public AutoMapper()
		{
			CreateMap<Event, EventCreateDto>().ReverseMap();
			CreateMap<Event, EventUpdateDto>().ReverseMap();
			CreateMap<Association, AssociationCreateDto>().ReverseMap();
			CreateMap<EventGetResponse, EventViewDto >().ReverseMap();
			CreateMap<EventCreateViewModel, EventCreateDto>();
			CreateMap<EventCreateDto, Event>();
			CreateMap<GetAssociationResponse, AssociationViewDto>().ReverseMap();
			CreateMap<BaseInfoDto, BaseInfoResponse>();
			CreateMap<BaseInfoResponse, BaseInfoDto>();
			CreateMap<AssociationUpdateDto, UpdateAssociationViewModel>().ReverseMap()
				.ForMember(res => res.LogoUrl, m => m
					.MapFrom(u => (string)null));

			CreateMap<CreateAssociationViewModel, AssociationCreateDto>()
				.ForMember(res => res.LogoUrl, m => m
					.MapFrom(u =>(string)null));

			/*CreateMap<AssociationCreateDto, CreateAssociationViewModel>()
				.ForMember(res => res.Logo, m => m
					.MapFrom(u => (IFormFile)null));*/




			//CreateMap<List<AssociationViewDto>, List<GetAssociationResponse>>().ReverseMap();
		}
	}
}
