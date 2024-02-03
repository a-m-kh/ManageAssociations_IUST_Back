using DataBase.Configuration.Domain;
using DataBase.Repository.Repositories.Interface;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repository.Repositories
{
	public class EventRepository: GeneralRepository<Event>,IEventRepository
	{
		private readonly IMapper _mapper;
		public EventRepository(IUnitOfWork uow , IMapper mapper) : base(uow)
		{
			_mapper = mapper;
		}

		public async Task<EventViewDto> CreateAsync(EventCreateDto Model)
		{
			if (Model == null)
				return null;

			var newEvent = _mapper.Map<Event>(Model);
			if (newEvent == null)
				return null;

			var DbEntity = await TEntity.AddAsync(newEvent);
			if (DbEntity == null)
				return null;

			return(_mapper.Map<EventViewDto>(DbEntity));
		}

		public async Task<bool> UpdateAsync(EventUpdateDto Model)
		{
			if (Model == null)
				return false;
			var entity = await TEntity.Where(a => a.ID == Model.ID && !a.IsDelete).FirstOrDefaultAsync();
			if(entity == null)
				return false;

			entity.Period = Model.Period == null ? (entity.Period) : (Model.Period ?? 0);
			entity.TypeOfEvent = Model.TypeOfEvent == null ? (entity.TypeOfEvent) : (Model.TypeOfEvent ?? 0);
			entity.Issue = Model.Issue == null ? (entity.Issue) : (Model.Issue ?? 0);
			entity.Title = Model.Title == null ? (entity.Title) : (Model.Title) ;
			entity.Price = Model.Price == null ? (entity.Price) : (Model.Price ?? 0);
			entity.StartTime = Model.StartTime == null ? (entity.StartTime) : (Model.StartTime ?? DateTime.Now);
			entity.EndTime = Model.EndTime == null ? (entity.EndTime) : (Model.EndTime ?? DateTime.Now);
			entity.ImageUrl = Model.ImageUrl == null ? (entity.ImageUrl) : (Model.ImageUrl);
			entity.Description = Model.Description == null ? (entity.Description) : (Model.Description);

			var IsUpdate = await _uow.SaveChangesAsync();

			return(IsUpdate > 0);

		}

		public async Task<bool> DeleteAsync(int Id)
		{
			var entity = await TEntity.Where(a => a.ID == Id && !a.IsDelete).FirstOrDefaultAsync();
			if(entity == null) return false;
			entity.IsDelete = true;
			var IsUpdate = await _uow.SaveChangesAsync();
			return(IsUpdate > 0);
		}

		public async Task<EventViewDto> GetByIdAsync(int Id)
		{
			var entity = await TEntity.Where(a => a.ID == Id && !a.IsDelete).FirstOrDefaultAsync();
			if (entity == null)
				return null;
			return (_mapper.Map<EventViewDto>(entity));

		}

		public async Task<List<EventViewDto>> GetByAssociationIdAsync(int Id, int Page = 1)
		{
			var total =  TEntity.Where(a => a.association.ID == Id && a.IsDelete).Count();
			var entities = TEntity.Where(a => !a.IsDelete && a.association.ID == Id)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4);
			return (_mapper.Map<List<EventViewDto>>(entities));
		}

		public async Task<List<EventViewDto>> GetLastEvent(int Page = 1)
		{
			var total = TEntity.Where(a => a.IsConfirm && a.IsDelete).Count();
			var entities = TEntity.Where(a => !a.IsDelete && a.IsConfirm)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4);
			return (_mapper.Map<List<EventViewDto>>(entities));
		}
	}
}
