using AutoMapper;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Mapper
{
	public class AutoMapper : Profile
	{
		public AutoMapper() 
		{
			CreateMap<Event , EventCreateDto>().ReverseMap();
			CreateMap<Event , EventUpdateDto>().ReverseMap();
			CreateMap<EventViewDto, EntityEntry<Event>>()
				.ForMember(a => a.Entity.Issue,
				m => m.MapFrom(u => u.Issue))
				.ForMember(a => a.Entity.EndTime,
					m => m.MapFrom(u => u.EndTime))
				.ForMember(a => a.Entity.StartTime,
					m => m.MapFrom(u => u.StartTime))
				.ForMember(a => a.Entity.ImageUrl,
					m => m.MapFrom(u => u.ImageUrl))
				.ForMember(a => a.Entity.Description,
					m => m.MapFrom(u => u.Description))
				.ForMember(a => a.Entity.Title,
					m => m.MapFrom(u => u.Title))
				.ForMember(a => a.Entity.ID,
				m => m.MapFrom(u => u.ID))
				.ForMember(a => a.Entity.Price,
					m => m.MapFrom(u => u.Price))
				.ForMember(a => a.Entity.Period,
					m => m.MapFrom(u => u.Period))
				.ForMember(a => a.Entity.TypeOfEvent,
					m => m.MapFrom(u => u.TypeOfEvent))
				.ReverseMap();

				
		}
	}
}
